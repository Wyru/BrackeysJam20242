//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Inputs/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""cace73f2-71cc-4565-b6ab-57d58e95445f"",
            ""actions"": [
                {
                    ""name"": ""interact"",
                    ""type"": ""Button"",
                    ""id"": ""1ea74818-6ccb-4ab8-b6f6-5e037b8cd9bd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""3888b4cf-0d95-4228-91bb-4e9ca262baef"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""look"",
                    ""type"": ""Value"",
                    ""id"": ""8e567f83-6a6a-4b52-b010-0b73302b62d9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""atack"",
                    ""type"": ""Button"",
                    ""id"": ""31dd0018-a438-42d1-a4d6-1d94636014cf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""throwWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""56c4cee3-b600-4459-85aa-03416799b00f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""drop"",
                    ""type"": ""Button"",
                    ""id"": ""27766205-2482-4ce5-b4b3-ca22e1f41ca4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""remove"",
                    ""type"": ""Button"",
                    ""id"": ""5197fbb6-538e-4824-917b-0f211acff271"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""6eb1b683-3beb-4512-9ae9-97d583b448ef"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""PlayerInputs"",
                    ""action"": ""interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""bd189509-2794-4e09-b8f8-ca3aeacb6b35"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""cd42d95e-adf3-4ab4-9e3d-5666816517a2"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""494b7680-03f4-43fd-9c6a-bcfc1eeae0f8"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""5292c7ed-d008-47d8-bf6d-803c82f7913e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""a60a9bdc-8343-488b-bfe7-5a8f3d003ced"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""24b98475-6c52-4ccc-9e57-6761b9c04734"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d334187-f1f8-4a29-9778-686439da7eae"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""atack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1a80a00b-dc47-4ea9-adca-7cb0aa4d2bdd"",
                    ""path"": ""<Keyboard>/x"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""drop"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ec6181b1-260c-4e83-93b7-2511b9724442"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""throwWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ef6a9107-8e72-44a8-a5e4-a85da6f3cc73"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""remove"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""PlayerInputs"",
            ""bindingGroup"": ""PlayerInputs"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_interact = m_Gameplay.FindAction("interact", throwIfNotFound: true);
        m_Gameplay_move = m_Gameplay.FindAction("move", throwIfNotFound: true);
        m_Gameplay_look = m_Gameplay.FindAction("look", throwIfNotFound: true);
        m_Gameplay_atack = m_Gameplay.FindAction("atack", throwIfNotFound: true);
        m_Gameplay_throwWeapon = m_Gameplay.FindAction("throwWeapon", throwIfNotFound: true);
        m_Gameplay_drop = m_Gameplay.FindAction("drop", throwIfNotFound: true);
        m_Gameplay_remove = m_Gameplay.FindAction("remove", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }

    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_interact;
    private readonly InputAction m_Gameplay_move;
    private readonly InputAction m_Gameplay_look;
    private readonly InputAction m_Gameplay_atack;
    private readonly InputAction m_Gameplay_throwWeapon;
    private readonly InputAction m_Gameplay_drop;
    private readonly InputAction m_Gameplay_remove;
    public struct GameplayActions
    {
        private @PlayerInput m_Wrapper;
        public GameplayActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @interact => m_Wrapper.m_Gameplay_interact;
        public InputAction @move => m_Wrapper.m_Gameplay_move;
        public InputAction @look => m_Wrapper.m_Gameplay_look;
        public InputAction @atack => m_Wrapper.m_Gameplay_atack;
        public InputAction @throwWeapon => m_Wrapper.m_Gameplay_throwWeapon;
        public InputAction @drop => m_Wrapper.m_Gameplay_drop;
        public InputAction @remove => m_Wrapper.m_Gameplay_remove;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @interact.started += instance.OnInteract;
            @interact.performed += instance.OnInteract;
            @interact.canceled += instance.OnInteract;
            @move.started += instance.OnMove;
            @move.performed += instance.OnMove;
            @move.canceled += instance.OnMove;
            @look.started += instance.OnLook;
            @look.performed += instance.OnLook;
            @look.canceled += instance.OnLook;
            @atack.started += instance.OnAtack;
            @atack.performed += instance.OnAtack;
            @atack.canceled += instance.OnAtack;
            @throwWeapon.started += instance.OnThrowWeapon;
            @throwWeapon.performed += instance.OnThrowWeapon;
            @throwWeapon.canceled += instance.OnThrowWeapon;
            @drop.started += instance.OnDrop;
            @drop.performed += instance.OnDrop;
            @drop.canceled += instance.OnDrop;
            @remove.started += instance.OnRemove;
            @remove.performed += instance.OnRemove;
            @remove.canceled += instance.OnRemove;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @interact.started -= instance.OnInteract;
            @interact.performed -= instance.OnInteract;
            @interact.canceled -= instance.OnInteract;
            @move.started -= instance.OnMove;
            @move.performed -= instance.OnMove;
            @move.canceled -= instance.OnMove;
            @look.started -= instance.OnLook;
            @look.performed -= instance.OnLook;
            @look.canceled -= instance.OnLook;
            @atack.started -= instance.OnAtack;
            @atack.performed -= instance.OnAtack;
            @atack.canceled -= instance.OnAtack;
            @throwWeapon.started -= instance.OnThrowWeapon;
            @throwWeapon.performed -= instance.OnThrowWeapon;
            @throwWeapon.canceled -= instance.OnThrowWeapon;
            @drop.started -= instance.OnDrop;
            @drop.performed -= instance.OnDrop;
            @drop.canceled -= instance.OnDrop;
            @remove.started -= instance.OnRemove;
            @remove.performed -= instance.OnRemove;
            @remove.canceled -= instance.OnRemove;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    private int m_PlayerInputsSchemeIndex = -1;
    public InputControlScheme PlayerInputsScheme
    {
        get
        {
            if (m_PlayerInputsSchemeIndex == -1) m_PlayerInputsSchemeIndex = asset.FindControlSchemeIndex("PlayerInputs");
            return asset.controlSchemes[m_PlayerInputsSchemeIndex];
        }
    }
    public interface IGameplayActions
    {
        void OnInteract(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnAtack(InputAction.CallbackContext context);
        void OnThrowWeapon(InputAction.CallbackContext context);
        void OnDrop(InputAction.CallbackContext context);
        void OnRemove(InputAction.CallbackContext context);
    }
}
