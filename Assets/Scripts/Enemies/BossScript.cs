using UnityEngine;

public class BossScript : MonoBehaviour
{
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.CallVictory();
        }
    }
}
