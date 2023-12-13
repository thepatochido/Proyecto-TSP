using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatBoneScript : MonoBehaviour
{

    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private GameObject boneItem1;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        navMeshAgent.speed = gameObject.GetComponent<HowlAndAttackScript>().chaseSpeed;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void FollowBoneStart()
    {
        boneItem1 = gameObject.GetComponent<HowlAndAttackScript>().boneItem;
        animator.SetBool("Idle", false);
        animator.SetBool("Run", true);
        animator.SetBool("Howl", false);
        animator.SetBool("Attack1", false);
        animator.SetBool("Attack2", false);
        animator.SetBool("Damage", false);
        animator.SetBool("Eat", false);
    }
}
