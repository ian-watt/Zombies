using System;
using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class Enemy : Actor
{
    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;


    public int health;


    public float timeBetweenAttacks;
    bool alreadyAttacked;

    public float attackRange;
    public bool playerInAttackRange;

    protected virtual void InitializeReferences()
    {
        player = GameObject.Find("Player Root").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    protected virtual void CheckAttackRange()
    {
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInAttackRange) ChasePlayer();
        if (playerInAttackRange) AttackPlayer();
    }

    protected virtual void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    protected virtual void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if (!alreadyAttacked)
        {
            Debug.Log("Attacked by Zombie");

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
        
    }

    protected void ResetAttack()
    {
        alreadyAttacked = false;
    }

    protected void Death()
    {
        if (health == 0)
        {
            Destroy(gameObject);
        }
    }
}
