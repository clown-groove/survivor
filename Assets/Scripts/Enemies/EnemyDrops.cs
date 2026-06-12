using NUnit.Framework;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot = new Loot[] { };

    private void OnDestroy()
    {
        float rng = Random.Range(0, 1);
        foreach( Loot loot in loot)
        {
            if (rng < loot.chance)
            {
                Instantiate(loot.item, transform.position, Quaternion.identity, null);
            }
        }
    }
}
 
public struct Loot
{
    public GameObject item;
    public float chance;
}
