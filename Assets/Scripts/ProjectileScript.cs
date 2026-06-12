using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ProjectileScript : MonoBehaviour
{
    private Vector2 startPosition;
    private float damage;
    private float bulletRange;
    private float knockback;
    
    public void SetProjectileStats(float damage, float bulletRange, float knockback)
    {
        this.damage = damage;
        this.bulletRange = bulletRange;
        this.knockback = knockback;
        startPosition = transform.position;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void Update()
    {
        if (((Vector2)transform.position - startPosition).magnitude >= bulletRange)
        {
            DestroyBullet();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*if (collision.GetComponent<EnemyHealth>())
        {

        }
        else*/ if (collision.GetComponent<PlayerHealth>())
        {
            collision.GetComponent<PlayerHealth>().ApplyDamage();
        }
        DestroyBullet();
    }
}
