using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static event Action<int> OnCurrentHealthChanged = delegate { };
    public static event Action<int, int> OnMaxHealthChanged = delegate { };

    private PlayerStats playerStats;

    [SerializeField]
    private int currentHealth;
    public int CurrentHealth { get { return currentHealth; } }

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

    public void ApplyDamage()
    {
        currentHealth -= 1;

        OnCurrentHealthChanged?.Invoke(currentHealth);
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (currentHealth <= 0)
        {
            //przegrana
        }
    }

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
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
