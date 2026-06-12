using UnityEngine;

public class PickupHeal : Pickup
{
    protected override void OnPickedUp(GameObject player)
    {
        player.GetComponent<PlayerHealth>().ApplyHeal();

        base.OnPickedUp(player);
    }
}
