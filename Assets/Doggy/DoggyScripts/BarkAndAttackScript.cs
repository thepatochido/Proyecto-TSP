using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class BarkAndAttackScript : MonoBehaviour
{
    private AudioSource audioSource;
    private NavMeshAgent navMeshAgent;
    private GameObject player;
    private Animator animator;

    public AudioClip[] howlClips;
    public AudioClip[] attackClips;
    public AudioClip[] barkClips;

    public int attackIndex;

    public bool howlStarted;
    public bool startPursuit = false;
    public bool howlFinnished;
    public bool followPlayer;
    public bool isAttacking;

    public float chaseRange;
    public float attackRange;
    public float chaseSpeed;
    public float stopFollowDelayTime;
    public float howlFinnishedDelayTime;
    public float timeBetweenAttacks;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.speed = chaseSpeed;
    
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (!startPursuit)
        {
            Idle();
            return;
        }

        if(startPursuit && !howlStarted)
        {
            Howl();
        }
        if (startPursuit && howlFinnished)
        {
            FollowPlayer();

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

    private void FollowPlayer()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run",true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        followPlayer = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void Howl()
    {
        howlStarted=true;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", true);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        int howlIndex = Random.Range(0, 2);
        audioSource.PlayOneShot(howlClips[howlIndex]);
        StartCoroutine(HowlFinnishedDelay());
    }

    private void HowlFinnished()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);
        howlFinnished=true;
    }

    private void Attack()
    {
        isAttacking = true;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        attackIndex = Random.Range(1, 10);

        if (attackIndex <= 5)
        {
            animator.SetBool("Attack1", true);
            animator.SetBool("Attack2", false);
        }
        else if (attackIndex <= 10)
        {
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", true);
        }

        navMeshAgent.isStopped = true;

        int attackSoundIndex = Random.Range(0, 2);
        audioSource.PlayOneShot(attackClips[attackSoundIndex]);

        StartCoroutine(DelayBetweenAttack());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            startPursuit = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            StartCoroutine(StopFollowDelay());
        }
    }

    IEnumerator HowlFinnishedDelay()
    {
        yield return new WaitForSeconds(howlFinnishedDelayTime);
        HowlFinnished();
    }

    IEnumerator StopFollowDelay()
    {
        yield return new WaitForSeconds(stopFollowDelayTime);
        followPlayer = false;
        startPursuit = false;

        int howlState = Random.Range(0, 1);
        bool howlStateBool = false;
        if (howlState !=0)
        {
            howlStateBool = true;
        }
        howlStarted = howlStateBool;

        howlFinnished = false;
    }

    IEnumerator DelayBetweenAttack()
    {
        yield return new WaitForSeconds(timeBetweenAttacks);
        isAttacking = false;

    }
}