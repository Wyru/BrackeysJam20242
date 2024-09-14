using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Rendering;

public class ComputerController : MonoBehaviour, IInteractable
{
    [SerializeField]
    public Transform _screen;
    public GameObject _screenCamera;
    private Canvas _crosshairCanvas;
    public Canvas _terminalUI;
    public Volume _pixelation;

    void Start()
    {
        _crosshairCanvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
    }

    public void Interact(){
        _pixelation.gameObject.SetActive(true);
        _crosshairCanvas.enabled = false;
        _terminalUI.gameObject.SetActive(true);
        PlayerController.instance.gameObject.SetActive(false);
        _screenCamera.tag = "MainCamera";
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        int changeLayer = LayerMask.NameToLayer("Default");
        _screen.gameObject.layer = changeLayer;
    }

    public void LeaveScreen()
    {
        int changeLayer = LayerMask.NameToLayer("Interactable");
        _pixelation.gameObject.SetActive(false);
        _crosshairCanvas.enabled = true;
        _terminalUI.gameObject.SetActive(false);
        PlayerController.instance.gameObject.SetActive(true);
        _screenCamera.tag = "Untagged";
        _screen.gameObject.layer = changeLayer;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
