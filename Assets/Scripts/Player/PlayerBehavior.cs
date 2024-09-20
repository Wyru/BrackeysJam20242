using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : StateMachine
{
    [Header("References")]
    public PlayerCameraBehavior playerCameraBehavior;
    public new Rigidbody rigidbody;
    public Animator animator;
    public PlayerWeaponBehavior playerWeapon;
    public PlayerItemBehavior playerItem;

    [Header("States")]
    public IdlePlayerState idlePlayerState;
    public WalkingPlayerState walkingPlayerState;

    [Header("Input References")]
    public InputActionReference cameraMovementInput;
    public InputActionReference movementInput;
    public InputActionReference interactInput;
    public InputActionReference dropWeaponInput;
    public InputActionReference dropItemInput;
    public InputActionReference useItemInput;
    public InputActionReference action1Input;
    public InputActionReference action2Input;

    [Header("Input Variable")]
    public Vector2 cameraMovement;
    public Vector2 movement;

    public static PlayerBehavior instance;

    protected virtual void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    override protected void Start()
    {
        base.Start();
        playerCameraBehavior = GetComponentInChildren<PlayerCameraBehavior>();
        playerCameraBehavior.LockMouse();
        currentState = initialState;
    }

    override protected void Update()
    {
        base.Update();
        UpdateInput();

    }

    override protected void FixedUpdate()
    {
        base.FixedUpdate();
    }

    override protected State SelectNextState()
    {
        return currentState.Next();
    }

    void UpdateInput()
    {
        cameraMovement = cameraMovementInput.action.ReadValue<Vector2>();
        movement = movementInput.action.ReadValue<Vector2>();
    }

    public void UpdateMovement(float speed)
    {
        rigidbody.velocity =
            movement.y * speed * transform.forward +
            movement.x * speed * transform.right +
            transform.up * rigidbody.velocity.y;
    }

    public void UpdateCamera()
    {
        playerCameraBehavior.UpdateCamera(cameraMovement);
    }
}
