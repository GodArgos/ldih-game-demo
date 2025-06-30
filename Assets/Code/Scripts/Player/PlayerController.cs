using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Dependencies")]
    public Rigidbody2D Rigidbody;
    public Animator Animator;
    public Transform InteractionPoint;
    [SerializeField] private FieldOfViewController fov;

    [Header("Parameters")]
    [SerializeField] private float speed = 5f;

    private PlayerMovement playerMovement;
    private PlayerAnimation playerAnimation;
    private PlayerInteractor playerInteractor;
    private DefaultInputSystem inputSystem;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerAnimation = GetComponent<PlayerAnimation>();
        playerInteractor = GetComponent<PlayerInteractor>();
        inputSystem = new DefaultInputSystem();
    }

    private void Update()
    {
        fov.SetOrigin(transform.position);
    }

    private void FixedUpdate()
    {
        Vector2 movementInput = inputSystem.Player.Move.ReadValue<Vector2>();

        playerMovement.Move(movementInput, speed);
        playerAnimation.UpdateAnimation(movementInput);
        playerInteractor.SetDirection(movementInput);
    }

    #region Input System
    private void OnEnable()
    {
        inputSystem.Enable();
        inputSystem.Player.Interact.performed += playerInteractor.HandleInteraction;
    }

    private void OnDisable()
    {
        inputSystem.Disable();
        inputSystem.Player.Interact.performed -= playerInteractor.HandleInteraction;
    }
    #endregion
}
