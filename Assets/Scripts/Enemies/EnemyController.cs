using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private EnemyStatsSO enemyStats;

    private Rigidbody2D rb;

    private float shootCooldown;

    private Vector2 playerDirection;

    public float GetMaxHealth()
    {
        return enemyStats.Stats[StatTypes.maxHealth];
    }

    private void HandleAim()
    {
        playerDirection = PlayerController.Instance.transform.position - transform.position;
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
        GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.LookRotation(playerDirection), null);
        spawnedBullet.GetComponent<ProjectileScript>().SetProjectileStats(
            0,
            enemyStats.Stats[StatTypes.bulletRange],
            enemyStats.Stats[StatTypes.knockback]
            );

        spawnedBullet.GetComponent<Rigidbody2D>().linearVelocity = enemyStats.Stats[StatTypes.bulletSpeed] * spawnedBullet.transform.right;
    }

    private void HandleMovement()
    {
        Vector2 moveDirection = playerDirection - (playerDirection * enemyStats.Stats[StatTypes.desiredDistanceFromPlayer]);

        float walkSpeed = enemyStats.Stats[StatTypes.walkSpeed];
        Vector2 calculatedSpeed = moveDirection.normalized * walkSpeed;
        float distanceToTarget = (calculatedSpeed - rb.linearVelocity).magnitude;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, calculatedSpeed, walkSpeed * 2.4f * Time.fixedDeltaTime * (1 + distanceToTarget));
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        enemyStats.InitializeStats();
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
}
