using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class InputManager : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInput.GameplayActions _gameplayActions;
    private PlayerController _playerController;
    public static InputManager instance;
    private GameManager _gameManager;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _gameplayActions = _playerInput.Gameplay;
        _playerController = GetComponent<PlayerController>();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        _playerController.ProcessMove(_gameplayActions.move.ReadValue<Vector2>());
        _playerController.ProcessAttack(_gameplayActions.atack.IsPressed());
        _playerController.DropWeapon(_gameplayActions.drop.IsPressed());
        _playerController.Throwing(_gameplayActions.throwWeapon.IsPressed());

        if (Input.GetKeyDown(KeyCode.P))
        {
            _gameManager.IncrementDay();
        }
    }

    public void OnGameEnd()
    {

    }

    public void OnEnable()
    {
        _gameplayActions.Enable();
    }

    public void OnDisable()
    {
        _gameplayActions.Disable();
    }
}
