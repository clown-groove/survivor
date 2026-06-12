using UnityEngine;

public class Pickup : MonoBehaviour
{
    protected virtual void OnPickedUp(GameObject player)
    {
        Destroy(gameObject);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        OnPickedUp(collision.gameObject);
    }
}
