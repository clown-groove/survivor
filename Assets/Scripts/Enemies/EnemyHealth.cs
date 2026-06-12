using UnityEngine;

[RequireComponent(typeof(PlayerController))]
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

    private void Start()
    {
        currentHealth = GetComponent<EnemyController>().GetMaxHealth();
    }
}
