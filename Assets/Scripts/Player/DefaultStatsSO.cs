using UnityEngine;

[CreateAssetMenu(fileName = "DefaultStats_x", menuName = "Scriptable Objects/Default Stats")]
public class DefaultStatsSO : ScriptableObject
{
    public Stat[] defaultStats = new Stat[]
    {
        new Stat { type = StatTypes.damage, value = 10f },
        new Stat { type = StatTypes.bulletSize, value = 1f },
        new Stat { type = StatTypes.fireRate, value = 3f },
        new Stat { type = StatTypes.bulletSpeed, value = 5f },
        new Stat { type = StatTypes.bulletRange, value = 10f },
        new Stat { type = StatTypes.reloadTime, value = 1.2f },
        new Stat { type = StatTypes.maxAmmo, value = 12f },
        new Stat { type = StatTypes.spreadAngle, value = 10f },
        new Stat { type = StatTypes.knockback, value = 1f },
        new Stat { type = StatTypes.walkSpeed, value = 4f },
        new Stat { type = StatTypes.maxHealth, value = 4f },
        new Stat { type = StatTypes.xpCollectRange, value = 4f },
    };
}
