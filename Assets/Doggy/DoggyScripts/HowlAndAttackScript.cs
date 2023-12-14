using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HowlAndAttackScript : MonoBehaviour
{
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private Animator animator;

    public AudioClip[] howlClips;
    public AudioClip[] attackClips;
    public AudioClip[] barkClips;
    public GameObject boneItem;

    public bool startPursuit;
    public bool howlStarted;
    public bool howlFinnished;
    public bool followPlayer;
    public bool isAttacking;
    public bool boneAviable;

    public float chaseRange;
    public float chaseRangeAmount;
    public float attackRange;
    public float eatRange;
    public float eatingTime;
    public float chaseSpeed;
    public float timeBetweenAttacks;

    public int attackAnimationIndex;
    public int damageToPlayer;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = chaseSpeed;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= chaseRange && boneAviable==false)
        {
            startPursuit = true;
        }

        if (boneAviable==true)
        {
            float distanceToBone = Vector3.Distance(transform.position, boneItem.transform.position);

            FollowBone();

            if (distanceToBone <= eatRange)
            {
                EatBone();
            }
            else
            {
                return;
            }
        }

        if (!startPursuit && boneAviable==false)
        {
            Idle();
            return;
        }

        if (startPursuit && !howlStarted)
        {
            Howl();
        }

        if (howlFinnished && startPursuit)
        {
            RunTowardsPlayer();
            
            if (distanceToPlayer < attackRange && !isAttacking)
            {
                Attack();
                StartCoroutine(DelayBetweenAttack());
            }
            else
            {
                return;
            }
        }

    }

    private void Idle()
    {
        animator.SetBool("Idle", true);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);
        navMeshAgent.isStopped = true;
    }

    private void Howl()
    {
        howlStarted = true;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", true);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        int howlIndex = Random.Range(0,howlClips.Length);
        audioSource.PlayOneShot(howlClips[howlIndex]);
    }

    private void RunTowardsPlayer()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        attackAnimationIndex = Random.Range(1, 10);

        if (attackAnimationIndex <= 5)
        {
            animator.SetBool("Attack1", true);
            animator.SetBool("Attack2", false);
        }
        else if (attackAnimationIndex <= 10)
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
        }

        navMeshAgent.isStopped = true;

        int attackSoundIndex = Random.Range(0, attackClips.Length);
        audioSource.PlayOneShot(attackClips[attackSoundIndex]);

        player.GetComponent<CorazonesScript>().DamagePlayer(damageToPlayer);

    }

    public void HowlFinnished()
    {
        howlFinnished= true;
    }

    private void FollowBone()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(boneItem.transform.position);
    }

    private void EatBone()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", true);

        navMeshAgent.isStopped = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("BoneForEat"))
        {
            boneItem = GameObject.FindGameObjectWithTag("BoneForEat");
            startPursuit = false;
            howlStarted= false;
            howlFinnished= false;
            followPlayer = false;
            isAttacking=false;
            boneAviable = true;
        }
    }

    IEnumerator DelayBetweenAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;
    }
}
