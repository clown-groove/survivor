using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> OnCurrentHealthChanged = delegate { };
    public static event Action<int, int> OnMaxHealthChanged = delegate { };

    private PlayerStats playerStats;
    private Rigidbody2D rb;

    [SerializeField]
    private float immunityTime = 1.3f;
    [SerializeField]
    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

    private bool immune;

    private void OnMaxHealthStatChanged(int max, int change)
    {
        OnMaxHealthChanged?.Invoke(max, change);
        if (change > 0)
        {
            ApplyHeal(change);
        }
        else if (change < 0)
        {
            if (currentHealth > max)
            {
                currentHealth = max;
            }
        }
    }

    public void ApplyHeal(int healAmmount = 1)
    {
        if(healAmmount < 1)
        {
            Debug.LogError("Zła ilość wyleczonych obrażeń");
            return;
        }
        currentHealth += healAmmount;

        if (currentHealth > playerStats.CurrentStats[StatTypes.maxHealth])
        {
            currentHealth = (int)playerStats.CurrentStats[StatTypes.maxHealth];
        }
        OnCurrentHealthChanged?.Invoke(currentHealth);
    }

    public void ApplyDamage(Vector2 dmgSourcePosition, float knockback)
    {
        if (!immune)
        {
            currentHealth -= 1;
            OnCurrentHealthChanged?.Invoke(currentHealth);

            rb.AddForce(((Vector2)transform.position - dmgSourcePosition).normalized * knockback, ForceMode2D.Impulse);

            StartCoroutine(ImmuneCoroutine());
        }

        CheckDeath();
    }

    private IEnumerator ImmuneCoroutine()
    {
        immune = true;
        yield return new WaitForSeconds(immunityTime);
        immune = false;
        yield return null;
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            if(GameManager.Instance != null)
            {
                GameManager.Instance.CallPlayerDead();
            }
        }
    }

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
        rb = GetComponent<Rigidbody2D>();
        immune = false;
    }

    private void Start()
    {
        currentHealth = (int)playerStats.CurrentStats[StatTypes.maxHealth];
    }

    private void OnEnable()
    {
        PlayerStats.OnMaxHealthChanged += OnMaxHealthStatChanged;
    }

    private void OnDisable()
    {
        PlayerStats.OnMaxHealthChanged -= OnMaxHealthChanged;
    }
}
