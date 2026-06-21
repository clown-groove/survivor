using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerWeapon))]
[RequireComponent(typeof(PlayerStats))]
public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    [SerializeField]
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private Rigidbody2D rb;
    private PlayerWeapon playerWeapon;
    private PlayerStats playerStats;

    private InputActionMap player;
    private InputAction movement;
    private InputAction attack;

    #region Update functions
    private void SetWalkVelocity(Vector2 inputDirection)
    {
        float walkSpeed = playerStats.CurrentStats[StatTypes.walkSpeed];
        Vector2 calculatedSpeed = (inputDirection.x * transform.right.normalized + inputDirection.y * transform.up.normalized).normalized * walkSpeed;
        float distanceToTarget = (calculatedSpeed - rb.linearVelocity).magnitude;
        rb.linearVelocity = Vector2.MoveTowards(rb.linearVelocity, calculatedSpeed, walkSpeed * 2.4f * Time.fixedDeltaTime * (1 + distanceToTarget));
        if (calculatedSpeed.magnitude < .1f)
        {
            animator.SetBool("isRunning", false);
        }
        else
        {
            animator.SetBool("isRunning", true);
        }

        spriteRenderer.flipX = inputDirection.x < 0 ? true : false;
    }
    #endregion

    #region Input events
    private void OnPause(InputAction.CallbackContext obj)
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.PauseInput();
        }
    }

    private void OnReload(InputAction.CallbackContext obj)
    {
        playerWeapon.CallReload();
    }

    private void OnAutoAim(InputAction.CallbackContext obj)
    {
        playerWeapon.AutoAimSwitch();
    }
    #endregion

    #region Callbacks
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        player = GetComponent<PlayerInput>().actions.FindActionMap("Player");
        rb = GetComponent<Rigidbody2D>();
        playerWeapon = GetComponent<PlayerWeapon>();
        playerStats = GetComponent<PlayerStats>();
        spriteRenderer = animator.GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        
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
        if (GameManager.Instance != null && GameManager.Instance.GamePaused)
        {
            return;
        }

        playerWeapon.ShootingInput(attack.IsPressed());
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance != null && GameManager.Instance.GamePaused)
        {
            return;
        }

        Vector2 inputDirection = movement.ReadValue<Vector2>();
        SetWalkVelocity(inputDirection);
    }
    #endregion
}
