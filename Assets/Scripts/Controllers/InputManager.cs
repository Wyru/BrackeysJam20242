using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.GameplayActions _gameplayActions;
    private PlayerController _playerController;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _gameplayActions = _playerInput.Gameplay;
        _playerController = GetComponent<PlayerController>();
    }

    private void FixedUpdate()
    {
        _playerController.ProcessMove(_gameplayActions.move.ReadValue<Vector2>());
        _playerController.ProcessAttack(_gameplayActions.atack.IsPressed());
        _playerController.DropWeapon(_gameplayActions.drop.IsPressed());
        _playerController.Throwing(_gameplayActions.throwWeapon.IsPressed());

    }

    private void OnEnable()
    {
        _gameplayActions.Enable();
    }

    private void OnDisable()
    {
        _gameplayActions.Disable();

    }
}
