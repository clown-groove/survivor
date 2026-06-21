using UnityEngine;
using UnityEngine.InputSystem.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private EnemyStatsSO enemyStats;

    private Rigidbody2D rb;

    private float shootCooldown;

    private Vector2 playerPosition;
    private Vector2 playerDirection;

    public void TriggerHurtAnimation()
    {
        animator.SetTrigger("Hurt");
    }

    private void HandleAim()
    {
        playerPosition = PlayerController.Instance.transform.position;
        playerDirection = playerPosition - (Vector2)transform.position;
    }

    private void HandleShooting()
    {
        if (enemyStats.Stats[StatTypes.fireRate] > 0)
        {
            if (enemyStats.Stats[StatTypes.bulletRange] / 1.5f >= playerDirection.magnitude) {
                if (shootCooldown <= 0f)
                {
                    shootCooldown = 1f / enemyStats.Stats[StatTypes.fireRate];
                    PerformShoot();
                }
            }
        }
        shootCooldown -= Time.deltaTime;
    }

    private void PerformShoot()
    {
        float angle = Mathf.Atan2(playerDirection.y, playerDirection.x) * Mathf.Rad2Deg;
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle), null);
        spawnedBullet.GetComponent<ProjectileScript>().SetProjectileStats(
            0,
            enemyStats.Stats[StatTypes.bulletRange],
            enemyStats.Stats[StatTypes.knockback]
            );

        spawnedBullet.GetComponent<Rigidbody2D>().linearVelocity = enemyStats.Stats[StatTypes.bulletSpeed] * spawnedBullet.transform.right;
    }

    private void HandleMovement()
    {
        Vector2 directionFromPlayer = playerDirection.normalized;

        Vector2 targetPosition = playerPosition - directionFromPlayer * enemyStats.Stats[StatTypes.desiredDistanceFromPlayer];
        Vector2 moveDirection = targetPosition - (Vector2)transform.position;

        float walkSpeed = enemyStats.Stats[StatTypes.walkSpeed];
        Vector2 calculatedSpeed = moveDirection.normalized * walkSpeed;

        if(moveDirection.magnitude < 0.1f)
        {
            calculatedSpeed = Vector2.zero;
        }
        
        float distanceToTarget = (calculatedSpeed - rb.linearVelocity).magnitude;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, calculatedSpeed, walkSpeed * 2.4f * Time.fixedDeltaTime * (1 + distanceToTarget));

        spriteRenderer.flipX = moveDirection.x < 0? true : false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (enemyStats.Stats.Count < 1)
            enemyStats.InitializeStats();
        GetComponent<EnemyHealth>().SetHealth(enemyStats.Stats[StatTypes.maxHealth]);
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.GamePaused)
        {
            return;
        }

        HandleAim();
        HandleShooting();
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.GamePaused)
        {
            return;
        }

        HandleMovement();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.rigidbody.GetComponent<PlayerHealth>())
        {
            collision.rigidbody.GetComponent<PlayerHealth>().ApplyDamage((Vector2)transform.position, enemyStats.Stats[StatTypes.knockback]);
        }
    }
}
