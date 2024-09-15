using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class StartSequence : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
    public GameObject _actualPlayer;
    public GameObject _canvas;
    public GameObject _hero;
    public Animator _anim;
    public TextMeshProUGUI text;
    public GameObject logo;
    // Start is called before the first frame update
    private void Awake()
    {
        _actualPlayer.SetActive(false);
        _canvas.SetActive(false);
        if (GameManager.instance.day > 1)
        {
            text.text = "DAY " + GameManager.instance.day.ToString();
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.anyKeyDown)
        {
            _anim.SetBool("Move/Click", true);
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(6.5f);
        text.enabled = false;
        logo.SetActive(false);
        GetComponent<CinemachineVirtualCamera>().enabled = false;
        // vcam.enabled = true;
        GetComponent<AudioSource>().volume = 0.1f;
        _actualPlayer.SetActive(true);
        _canvas.SetActive(true);
        yield return new WaitForSeconds(2f);
        Destroy(_hero);
        Destroy(this);
    }
}
