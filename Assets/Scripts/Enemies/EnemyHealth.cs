using UnityEngine;

[RequireComponent(typeof(EnemyController))]
[RequireComponent(typeof(EnemyDrops))]
public class EnemyHealth : MonoBehaviour
{
    private float currentHealth;

    public void ApplyDamage(float damage)
    {
        currentHealth -= damage;

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void SetHealth(float health)
    {
        currentHealth = health;
    }
}
