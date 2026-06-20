using UnityEngine;

public class EnemyDrops : MonoBehaviour
{
    [SerializeField]
    private Loot[] loot = new Loot[] { };

    public void SpawnDrops()
    {
        foreach( Loot loot in loot)
        {
            float rng = Random.Range(0, 1);
            if (rng < loot.chance)
            {
                Instantiate(loot.item, transform.position, Quaternion.identity, null);
            }
        }
    }
}

[System.Serializable]
public struct Loot
{
    public GameObject item;
    public float chance;
}
