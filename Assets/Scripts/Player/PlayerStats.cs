using System;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static event Action<int, int> OnMaxHealthChanged = delegate { };

    [SerializeField]
    private DefaultStatsSO defaultStats;
    public Dictionary<StatTypes, float> CurrentStats { get; private set; } = new Dictionary<StatTypes, float>();

    public void ModifyStatAmmount(StatTypes statType, float changeAmmount)
    {
        switch (statType)
        {
            case StatTypes.maxAmmo:
            case StatTypes.maxHealth:
            case StatTypes.bulletAmmount:
                CurrentStats[statType] += changeAmmount;
                CurrentStats[statType] = (int)CurrentStats[statType];
                if (CurrentStats[statType] < 1)
                {
                    CurrentStats[statType] = 1;
                }
                break;
            default:
                Debug.LogError("Nieprawidłowy sposób zmiany statystyki. Wykorzystano ammount zamiast multiplier");
                break;
        }

        if(statType == StatTypes.maxHealth)
        {
            OnMaxHealthChanged?.Invoke((int)CurrentStats[StatTypes.maxHealth], (int)changeAmmount);
        }
    }

    public void ModifyStatMultiplier(StatTypes statType, float changeMultiplier)
    {
        switch (statType)
        {
            case StatTypes.damage:
            case StatTypes.bulletSize:
            case StatTypes.fireRate:
            case StatTypes.bulletSpeed:
            case StatTypes.bulletRange:
            case StatTypes.reloadTime:
            case StatTypes.spreadAngle:
            case StatTypes.knockback:
            case StatTypes.walkSpeed:
            case StatTypes.xpCollectRange:
                CurrentStats[statType] *= changeMultiplier;
                if (CurrentStats[statType] <= 0)
                {
                    CurrentStats[statType] = 0.01f;
                }
                break;
            default:
                Debug.LogError("Nieprawidłowy sposób zmiany statystyki. Wykorzystano multiplier zamiast ammount");
                break;
        }
    }

    private void Start()
    {
        foreach (Stat stat in defaultStats.defaultStats)
        {
            CurrentStats.Add(stat.type, stat.value);
        }

        OnMaxHealthChanged?.Invoke((int)CurrentStats[StatTypes.maxHealth], (int)CurrentStats[StatTypes.maxHealth]);
    }
}