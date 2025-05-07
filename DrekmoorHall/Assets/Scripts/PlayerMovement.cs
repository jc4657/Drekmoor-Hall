using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IDamagable
{
    [Header("Movement Settings")]
    public float moveSpeed = 15;
    public float jumpForce = 25;
    [SerializeField] private float friction = 10;
    [SerializeField] private float gravity = 3;

    [Header("Health Settings")]
    public int maxHealth = 100;
    private int currentHealth;

    [Header("References")]
    public Camera playerCamera;

    private Rigidbody rigidBody;
    private Vector3 moveDirection = Vector3.zero;
    private float offset = 1;

    //private PlayerInputActions inputActions;
    private Vector2 moveInput;
    private bool jumpPressed;

    public static PlayerMovement Instance;

    void Awake()
    {
        Instance = this;
        //inputActions = new PlayerInputActions();

        //inputActions.Player.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        //inputActions.Player.Move.canceled += ctx => moveInput = Vector2.zero;
        //inputActions.Player.Jump.performed += ctx => jumpPressed = true;
    }

    void OnEnable()
    {
        //inputActions.Player.Enable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void OnDisable()
    {
       // if (inputActions != null)
        //    inputActions.Player.Disable();
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        moveDirection = (transform.forward * moveInput.y + transform.right * moveInput.x).normalized;

        if (jumpPressed && IsGrounded())
        {
            rigidBody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpPressed = false;
        }
    }

    void FixedUpdate()
    {
        rigidBody.AddForce(moveDirection * moveSpeed / 10f, ForceMode.Impulse);
        rigidBody.linearVelocity = new Vector3(
            rigidBody.linearVelocity.x * (100 - friction) / 100,
            rigidBody.linearVelocity.y - gravity / 10,
            rigidBody.linearVelocity.z * (100 - friction) / 100
        );
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, offset + 0.15f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log($"Player took {damage} damage. Health left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        //Debug.Log("Player died!");
        //MySceneManager.CleanupInput();
        //MySceneManager.LoadDeath();
    }



}
