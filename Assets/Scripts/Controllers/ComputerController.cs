using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class ComputerController : MonoBehaviour, IInteractable
{
    public static ComputerController instance;
    [SerializeField]
    public Transform _screen;
    public GameObject _screenCamera;
    private GameObject _crossHair;
    private GameObject _possibleKeys;
    private GameObject _playerMoney;
    private Canvas _crosshairCanvas;
    public Canvas _terminalUI;
    public Volume _pixelation;
    public GameObject _UICamera;
    public UnityEvent OnInteract;

    void Start()
    {
        instance = this;
        _crosshairCanvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
        _crossHair = _crosshairCanvas.transform.Find("Crosshair").gameObject;
        _possibleKeys = _crosshairCanvas.transform.Find("PossibleKeys").gameObject;
        _playerMoney = _crosshairCanvas.transform.Find("PlayerMoney").gameObject;
    }

    public void Interact(){
        if(OfficeManager.instance.clockIn){
            OnInteract?.Invoke();
            _pixelation.gameObject.SetActive(true);
            _possibleKeys.SetActive(false);
            _crossHair.SetActive(false);
            _playerMoney.SetActive(false);
            _terminalUI.gameObject.SetActive(true);
            PlayerController.instance.gameObject.SetActive(false);
            _screenCamera.tag = "MainCamera";
            _UICamera.GetComponent<AudioListener>().enabled = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            int changeLayer = LayerMask.NameToLayer("Default");
            _screen.gameObject.layer = changeLayer;
        }
    }

    public void LeaveScreen()
    {
        int changeLayer = LayerMask.NameToLayer("Interactable");
        _pixelation.gameObject.SetActive(false);
        _possibleKeys.SetActive(true);
        _crossHair.SetActive(true);
        _playerMoney.SetActive(true);
        _UICamera.GetComponent<AudioListener>().enabled = false;
        _terminalUI.gameObject.SetActive(false);
        PlayerController.instance.gameObject.SetActive(true);
        _screenCamera.tag = "Untagged";
        _screen.gameObject.layer = changeLayer;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
