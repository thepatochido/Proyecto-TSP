using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class BarkAndAttackScript : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    public AudioClip[] barkClips;
    public AudioClip[] howlClips;
    public AudioClip[] attackClips;

    private int barkClipIndex;
    private int howlClipIndex;
    private int attackClipIndex;
    private int repeatIndex;

    public float followDelayTime;
    public float stopFollowDelayTime;
    public float barkDelay;

    public bool followPlayer = false;
    public bool startDelayStopFollow = false;
    public bool attackPlayer =false;
    public bool isAttacking = false;

    private void Start()
    {
        followPlayer = false;
        audioSource = GetComponent<AudioSource>();
        animator= GetComponent<Animator>();
        navMeshAgent= GetComponent<NavMeshAgent>();

    }

    void Update()
    {
        if (followPlayer == true)
        {
            FollowPlayer();
        }
    }

    //El jugador entra en el collider de detección
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Howl();
            StartCoroutine(FollowDelay(followDelayTime));
        }
    }

    //El jugador sale del collider de detección
    private void OnTriggerExit(Collider other)
    {
        startDelayStopFollow = true;
        StartCoroutine(DelayOnTriggerExit(stopFollowDelayTime));
    }

    //El perro ladra
    private void Bark()
    {
        repeatIndex = Random.Range(1, 3);

        //El perro ladra aleatoriamente de 1 a 4 veces
        for (int i = 0; i <= repeatIndex; i++)
        {
            barkClipIndex = Random.Range(0, 4);
            audioSource.PlayOneShot(barkClips[barkClipIndex]);
        }
    }

    //El perro aulla
    private void Howl()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", false);
        animator.SetBool("Howl", true);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        howlClipIndex = Random.Range(0, 1);
        audioSource.PlayOneShot(howlClips[howlClipIndex]);
    }

    //El perro persigue al jugador
    private void FollowPlayer()
    {
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);

        followPlayer = true;
        navMeshAgent.isStopped = false;
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent.SetDestination(playerObject.transform.position);
    }

    //El perro ataca al jugador y le resta un corazón (o le baja vida)
    private void Attack()
    {
        attackPlayer = true;

        animator.SetBool("Howl", false);
        animator.SetBool("Idle", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Run", false);
        animator.SetBool("Eat", false);

        int attackIndex = Random.Range(0, 1);
        switch(attackIndex)
        {
            case 0: animator.SetBool("Attack1", true);
                    animator.SetBool("Attack2", false);
                break;

            case 1: animator.SetBool("Attack1", false);
                    animator.SetBool("Attack2", true);
                break;
        }

        if (attackPlayer == true)
        {
            attackClipIndex = Random.Range(0, 1);
            audioSource.PlayOneShot(attackClips[attackClipIndex]);
        }
    }

    private void StopFollowPlayer()
    {
        attackPlayer = false;
        followPlayer = false;
        navMeshAgent.isStopped = true;
    }

    private void DamagePlayer(int DamageAmount)
    {
        //PlayerHealthScript.playerHealth -= AmountDamage;

    }

    private IEnumerator DelayOnTriggerExit(float time)
    {
        yield return new WaitForSeconds(time);

        if (followPlayer == true)
        {

            StopFollowPlayer();
            animator.SetBool("Howl", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Damage", false);
            animator.SetBool("Run", false);
            animator.SetBool("Eat", false);
            animator.SetBool("Attack1", false);
            animator.SetBool("Attack2", false);
        }

    }

    private IEnumerator FollowDelay(float time)
    {
        yield return new WaitForSeconds(time);
        followPlayer = true;
    }

    /*private IEnumerator BarkDelay(float time)
    {
        yield return new WaitForSeconds(time);
        Bark();
    }*/


}