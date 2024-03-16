using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyNPCAI : MonoBehaviour
{

    public NavMeshAgent agent;
    public Transform _player;
    public LayerMask ground, player;

    public Vector3 destinationPoint;
    private bool destinationPointSet;
    public float walkPointRange;

    public float timeBetweenAttacks;
    private bool alreadyAttacked;
    public GameObject sphere;

    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player);

        if (!playerInSightRange && !playerInAttackRange)
        {
            Patrolling();
        }
        if (playerInSightRange && !playerInAttackRange)
        {

            ChasePlayer();
        }
        if (playerInSightRange && playerInAttackRange)
        {
            AttackPlayer();
        }
    }

    public void Patrolling()
    {
        if (!destinationPointSet)
        {
            SearchWalkPoint();

        }

        if (destinationPointSet)
        {
            agent.SetDestination(destinationPoint);
        }

        Vector3 distanceToDestinationPoint = transform.position - destinationPoint;
        if (distanceToDestinationPoint.magnitude < 1.0f)
        {
            destinationPointSet = false;
        }

    }

    public void SearchWalkPoint()
    {
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        float randomZ = Random.Range(-walkPointRange, walkPointRange);

        destinationPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if (Physics.Raycast(destinationPoint, -transform.up, 2.0f, ground))
        {
            destinationPointSet = true;
        }
    }

    public void ChasePlayer()
    {
        agent.SetDestination(_player.position);

    }

    public void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(_player);

        if (!alreadyAttacked)
        {
            Rigidbody rb = Instantiate(sphere, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 25f, ForceMode.Impulse);
            rb.AddForce(transform.up * 7f, ForceMode.Impulse);
            alreadyAttacked = true;

            //StartCoroutine(Bullet());
            Invoke(nameof(resetAttack), timeBetweenAttacks);
        }
    }

    void resetAttack()
        {
            alreadyAttacked = false;
        }

}
