using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    // Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    // Animation and movement sync
    [SerializeField] private float walkSpeed = 2f;
    [SerializeField] private float attackAnimationDuration = 1.2f; // Duration of attack animation

    private EnemyDamage enemyDamage;
    private Target target;
    private Animator animator;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        enemyDamage = GetComponent<EnemyDamage>();
        target = GetComponent<Target>();
        animator = GetComponent<Animator>();

        agent.speed = walkSpeed;
    }

    private void Update()
    {
        if (target.health <= 0 || animator.GetBool("IsDead")) return; // Stop AI when dead

        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange) Patroling();
        if (playerInSightRange && !playerInAttackRange) ChasePlayer();
        if (playerInAttackRange && playerInSightRange) AttackPlayer();
    }

    private void Patroling()
    {
        if (!walkPointSet) SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
        }
        else
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("IsAttacking", false);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
        animator.SetBool("IsWalking", true);
        animator.SetBool("IsAttacking", false);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        animator.SetBool("IsWalking", false);
        animator.SetBool("IsAttacking", true);

        if (!alreadyAttacked)
        {
            enemyDamage.DealDamageToPlayer();
            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), attackAnimationDuration);
            Invoke(nameof(AllowNextAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsWalking", true); // Return to walking after attack
    }

    private void AllowNextAttack()
    {
        alreadyAttacked = false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}