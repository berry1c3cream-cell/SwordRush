using UnityEngine;

public class MEnemyBrain : MonoBehaviour
{
    public enum State { Patrol, Chase, Attack }
    public State currentState = State.Patrol;

    [Header("References")]
    public Transform[] patrolPoints;
    public Transform player;
    private PVidaPlayer vidaPlayer;

    [Header("SOLO PARA PRUEBAS/DEBUG!!!")]
    public float PlayerHealth = 100;

    [Header("Settings")]
    public float patrolSpeed = 2f;
    public float chaseSpeed = 4f;
    public float chaseDistance = 8f;
    public float attackDistance = 1.5f;
    public float attackCooldown = 1f;
    public float attackDamage = 1f;

    // Estos son solo para debugging y development
    //[Header("State Materials")]
    //public Material patrolMaterial;
    //public Material chaseMaterial;
    //public Material attackMaterial;
    //private Renderer rend;

    private int patrolIndex = 0;
    private float attackTimer = 0f;

    void Start()
    {
        //rend = GetComponent<Renderer>();
        //rend.material = patrolMaterial;

        vidaPlayer = player.GetComponent<PVidaPlayer>();

        if (vidaPlayer == null)
        {
            Debug.LogError("El Player no tiene PVidaPlayer.");
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Patrol:
                Patrol();
                break;

            case State.Chase:
                Chase();
                break;

            case State.Attack:
                Attack();
                break;
        }

        attackTimer -= Time.deltaTime;
    }

    // -------- PATROL -------- //
    void Patrol()
    {
        //rend.material = patrolMaterial;

        Transform point = patrolPoints[patrolIndex];
        MoveTowards(point.position, patrolSpeed);

        if (Vector3.Distance(transform.position, point.position) < 0.3f)
            patrolIndex = (patrolIndex + 1) % patrolPoints.Length;

        if (Vector3.Distance(transform.position, player.position) < chaseDistance)
            currentState = State.Chase;
    }

    // -------- CHASE -------- //
    void Chase()
    {
        //rend.material = chaseMaterial;

        MoveTowards(player.position, chaseSpeed);

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > chaseDistance + 2f)
            currentState = State.Patrol;

        if (dist < attackDistance)
            currentState = State.Attack;
    }

    // -------- ATTACK -------- //
    void Attack()
    {
        Vector3 lookPosition = new Vector3(
            player.position.x,
            transform.position.y,
            player.position.z
        );

        Vector3 direction = lookPosition - transform.position;

        if (direction != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(direction)
                                 * Quaternion.Euler(0, 180, 0);
        }

        float dist = Vector3.Distance(transform.position, player.position);

        if (dist > attackDistance)
        {
            currentState = State.Chase;
            return;
        }

        if (attackTimer <= 0f)
        {
            Debug.Log("El enemigo atacó!");

            if (vidaPlayer != null)
            {
                vidaPlayer.RecibirDaño((int)attackDamage);
            }

            attackTimer = attackCooldown;
        }
    }

    //void Attack()
    //{
    //    //rend.material = attackMaterial;

    //    Vector3 lookPosition = new Vector3(player.position.x,transform.position.y,player.position.z);

    //    transform.LookAt(lookPosition);
    //    float dist = Vector3.Distance(transform.position, player.position);

    //    if (dist > attackDistance)
    //    {
    //        currentState = State.Chase;
    //        return;
    //    }

    //    if (attackTimer <= 0f)
    //    {
    //        Debug.Log("El enemigo atacó!");

    //        if (vidaPlayer != null)
    //        {
    //            vidaPlayer.RecibirDaño((int)attackDamage);
    //        }

    //        attackTimer = attackCooldown;
    //    }
    //}

    // -------- MOVEMENT -------- //
    void MoveTowards(Vector3 target, float speed)
    {
        Vector3 targetPosition = new Vector3(
            target.x,
            transform.position.y,
            target.z
        );

        Vector3 dir = (targetPosition - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(dir) * Quaternion.Euler(0, 180, 0);
        }
    }

    //void MoveTowards(Vector3 target, float speed)
    //{
    //    Vector3 targetPosition = new Vector3(
    //        target.x,
    //        transform.position.y,
    //        target.z
    //    );

    //    Vector3 dir = (targetPosition - transform.position).normalized;

    //    transform.position += dir * speed * Time.deltaTime;
    //    transform.LookAt(targetPosition);
    //}

    // -------- GIZMOS -------- //
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseDistance);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
