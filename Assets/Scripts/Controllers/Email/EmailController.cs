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
    public Transform buttonArea;     
    public TMP_Text textArea;
    public TMP_Text senderTextArea;
    private Button lastClickedButton;  

    [SerializeField] private List<EmailScriptableObject> emails; 

    private void Start()
    {
        instance = this;
        InstatiateEmails();
    }

    private void InstatiateEmails()
    {
        foreach (EmailScriptableObject email in emails)
        {
            InstantiateButton(email);
        }
        emails.Clear();
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
}

