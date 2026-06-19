using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerWeapon : MonoBehaviour
{
    public static event Action<int> OnAmmoAmmountChange = delegate { };

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

    private Vector2 aimDirection;

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
        currentAmmo = (int)playerStats.CurrentStats[StatTypes.maxAmmo];
        isReloading = false;

        OnAmmoAmmountChange?.Invoke(currentAmmo);
        yield return null;
    }

    private void HandleAim()
    {
        aimDirection = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) - transform.position;
    }

    private void HandleShooting()
    {
        if (isShooting && !isReloading)
        {
            if(shootCooldown <= 0f && currentAmmo > 0)
            {
                shootCooldown = 1f / playerStats.CurrentStats[StatTypes.fireRate];
                PerformShoot();

                if (currentAmmo <= 0)
                {
                    CallReload();
                }
            }
        }
        shootCooldown -= Time.deltaTime;
    }

    private void PerformShoot()
    {
        for (int i = 0; i < playerStats.CurrentStats[StatTypes.bulletAmmount]; i++)
        {
            float spread = UnityEngine.Random.Range(-playerStats.CurrentStats[StatTypes.spreadAngle] / 2, playerStats.CurrentStats[StatTypes.spreadAngle] / 2);
            float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
            angle += spread;

            GameObject spawnedBullet = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, angle), null);
            spawnedBullet.GetComponent<ProjectileScript>().SetProjectileStats(
                playerStats.CurrentStats[StatTypes.damage],
                playerStats.CurrentStats[StatTypes.bulletRange],
                playerStats.CurrentStats[StatTypes.knockback]
                );

            spawnedBullet.GetComponent<Rigidbody2D>().linearVelocity = playerStats.CurrentStats[StatTypes.bulletSpeed] * spawnedBullet.transform.right;
        }

        currentAmmo -= 1;
        OnAmmoAmmountChange?.Invoke(currentAmmo);
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
