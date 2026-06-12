using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats_x", menuName = "Scriptable Objects/Enemy Stats")]
public class EnemyStatsSO : ScriptableObject
{
    [SerializeField]
    private Stat[] enemyStats = new Stat[]
    {
        new Stat { type = StatTypes.fireRate, value = 0f },
        new Stat { type = StatTypes.bulletSpeed, value = 5f },
        new Stat { type = StatTypes.bulletRange, value = 20f },
        new Stat { type = StatTypes.walkSpeed, value = 1.7f },
        new Stat { type = StatTypes.maxHealth, value = 10f },
        new Stat { type = StatTypes.knockback, value = 1f },
        new Stat { type = StatTypes.desiredDistanceFromPlayer, value = 0f },
    };

    public Dictionary<StatTypes, float> Stats { get; private set; } = new Dictionary<StatTypes, float>();

    public void InitializeStats()
    {
        foreach (Stat stat in enemyStats)
        {
            Stats.Add(stat.type, stat.value);
        }
    }
}
