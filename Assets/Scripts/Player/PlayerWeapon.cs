using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    private bool autoAiming;

    public void AutoAimSwitch()
    {
        autoAiming = !autoAiming;
        if (autoAiming)
        {
            
        }
        else
        {

        }
    }

    public void CallReload()
    {

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        autoAiming = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
