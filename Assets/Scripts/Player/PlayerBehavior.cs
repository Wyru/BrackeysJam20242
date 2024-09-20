using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : StateMachine
{
    public PlayerCameraBehavior playerCameraBehavior;
    public new Rigidbody rigidbody;

    [Header("Movement")]
    public float speed;

    [Header("Input")]
    public InputActionReference cameraMovementInput;
    public InputActionReference movementInput;


    [SerializeField] Vector2 cameraMovement;
    [SerializeField] Vector2 movement;

    override protected void Start()
    {
        base.Start();
        playerCameraBehavior = GetComponentInChildren<PlayerCameraBehavior>();
        playerCameraBehavior.LockMouse();

        // Ativa as InputActions
        // movementInput.action.Enable();
        // cameraMovementInput.action.Enable();
    }

    override protected void Update()
    {
        base.Update();
        cameraMovement = cameraMovementInput.action.ReadValue<Vector2>();
        movement = movementInput.action.ReadValue<Vector2>();
    }

    override protected void FixedUpdate()
    {
        base.FixedUpdate();

        playerCameraBehavior.UpdateCamera(cameraMovement);
        rigidbody.velocity =
            movement.y * speed * transform.forward +
            movement.x * speed * transform.right +
            transform.up * rigidbody.velocity.y;

    }
}
