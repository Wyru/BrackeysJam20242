using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmailController : MonoBehaviour
{
    public static EmailController instance;
    public GameObject buttonPrefab;
    public GameObject newEmailIcon;
    public Transform buttonArea;     
    public TMP_Text textArea;
    public TMP_Text senderTextArea;
    private Button lastClickedButton;
    private AudioSource soundSource;
    public AudioClip receiveEmailSound;

    [SerializeField] private List<EmailScriptableObject> emails; 

    private void Start()
    {
        instance = this;
        soundSource = GetComponent<AudioSource>();
        if(GameManager.instance.day > 1){
            emails = GameManager.instance.emailsAlreadyShowed;
        }
        InstatiateEmails();
    }

    private void InstatiateEmails()
    {
        foreach (EmailScriptableObject email in emails)
        {
            InstantiateButton(email);
            GameManager.instance.emailsAlreadyShowed.Add(email);
        }
        emails.Clear();
        newEmailIcon.gameObject.SetActive(true);
        PlaySound();
    }

    public void InstantiateButton(EmailScriptableObject email)
    {

        GameObject newButton = Instantiate(buttonPrefab, buttonArea);
        

        newButton.GetComponentInChildren<TMP_Text>().text = email.emailTitle;

        Button buttonComponent = newButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => OnButtonClick(buttonComponent, email));
    }


    private void OnButtonClick(Button clickedButton, EmailScriptableObject email)
    {

        if (lastClickedButton != null)
        {
            Text previousButtonText = lastClickedButton.GetComponentInChildren<Text>();
            if (previousButtonText != null)
            {
                previousButtonText.text = "";  
            }
        }

        textArea.text = email.emailContent;
        senderTextArea.text = "Sender: " + email.sender;

        lastClickedButton = clickedButton;
    }

    public void AddEmail(EmailScriptableObject newEmail)
    {
        emails.Add(newEmail);
        InstatiateEmails();
    }

    public void PlaySound()
    {
        soundSource.clip = receiveEmailSound;
        soundSource.Play();
    }
}

