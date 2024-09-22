using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehavior : StateMachine
{
    [Header("References")]
    public PlayerCameraBehavior playerCameraBehavior;
    public new Rigidbody rigidbody;
    public Animator armsAnimator;
    public Animator bodyAnimator;
    public PlayerWeaponBehavior playerWeapon;
    public PlayerItemBehavior playerItem;
    public PlayerFatigueBehavior fatigue;
    public PlayerAnimationEvents animationEvents;

    public static Animator ArmsAni
    {
        get
        {
            return instance.armsAnimator;
        }
    }


    public static Rigidbody Rb
    {
        get
        {
            return instance.rigidbody;
        }
    }

    public static PlayerWeaponBehavior Weapon
    {
        get
        {
            return instance.playerWeapon;
        }
    }

    public static PlayerFatigueBehavior Fatigue
    {
        get
        {
            return instance.fatigue;
        }
    }

    [Header("States")]
    public IdlePlayerState idlePlayerState;
    public WalkingPlayerState walkingPlayerState;
    public AttackPlayerState attackPlayerState;
    public CrouchingPlayerState crouchingPlayerState;
    public ThrowPlayerState throwPlayerState;

    [Header("Input References")]
    public InputActionReference cameraMovementInput;
    public InputActionReference movementInput;
    public InputActionReference runInput;
    public InputActionReference crouchInput;
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

    protected override void ChangeState(State state)
    {
        animationEvents.ClearAllEvents();
        base.ChangeState(state);
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
