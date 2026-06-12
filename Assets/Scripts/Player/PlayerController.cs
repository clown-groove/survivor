using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerWeapon playerWeapon;
    private PlayerStats playerStats;

    private InputActionMap player;
    private InputAction movement;
    private InputAction attack;

    private bool autoAiming;


    #region Input events
    private void OnPause(InputAction.CallbackContext obj)
    {

    }

    private void OnReload(InputAction.CallbackContext obj)
    {

    }

    private void OnAutoAim(InputAction.CallbackContext obj)
    {

    }
    #endregion

    #region Callbacks
    private void Awake()
    {
        player = GetComponent<PlayerInput>().actions.FindActionMap("Player");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerStats = GetComponent<PlayerStats>();

        autoAiming = false;
    }

    private void OnEnable()
    {
        player.FindAction("Pause").performed += OnPause;
        player.FindAction("Pause").Enable();
        player.FindAction("Reload").performed += OnReload;
        player.FindAction("Reload").Enable();
        player.FindAction("AutoAim").performed += OnAutoAim;
        player.FindAction("AutoAim").Enable();

        movement = player.FindAction("Move");
        movement.Enable();
        attack = player.FindAction("Attack");
        attack.Enable();
    }

    private void OnDisable()
    {
        player.FindAction("Pause").Disable();
        player.FindAction("Reload").Disable();
        player.FindAction("AutoAim").Disable();
        movement.Disable();
        attack.Disable();
    }

    private void Update()
    {
        
    }

    private void FixedUpdate()
    {
        
    }
    #endregion
}
