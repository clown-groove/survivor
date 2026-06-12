using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform weaponShootPoint;

    private PlayerStats playerStats;


    private bool autoAiming;
    private bool isShooting;
    private bool isReloading;

    private int currentAmmo;
    private float shootCooldown;

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

    public void ShootingInput(bool shooting)
    {
        isShooting = shooting;
    }

    public void CallReload()
    {
        if (currentAmmo < (int)playerStats.CurrentStats[StatTypes.maxAmmo] && !isReloading)
        {
            StartCoroutine(ReloadCoroutine());
        }
    }

    private IEnumerator ReloadCoroutine()
    {
        isReloading = true;
        //animacja przeładowania
        yield return new WaitForSeconds(playerStats.CurrentStats[StatTypes.reloadTime]);
        isReloading = false;
        currentAmmo = (int)playerStats.CurrentStats[StatTypes.maxAmmo];
        yield return null;
    }

    private void HandleAim()
    {
        Vector2 mauseWorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }

    private void HandleShooting()
    {
        if (isShooting)
        {
            if(shootCooldown <= 0f)
            {
                shootCooldown = 1f / playerStats.CurrentStats[StatTypes.fireRate];
                PerformShoot();
            }
        }
    }

    private void PerformShoot()
    {

    }

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();

        autoAiming = false;
        isReloading = false;
    }

    private void Start()
    {
        currentAmmo = (int)playerStats.CurrentStats[StatTypes.maxAmmo];
    }

    void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.GamePaused)
        {
            return;
        }

        HandleAim();
        HandleShooting();
    }
}
