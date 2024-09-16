using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EmailsManager : MonoBehaviour
{
    public int currentDay;
    public List<EmailScriptableObject> day1EmailSatisfaction;
    public List<EmailScriptableObject> day2EmailSatisfaction;
    public List<EmailScriptableObject> day3EmailSatisfaction;
    public List<EmailScriptableObject> day4EmailSatisfaction;
    public List<EmailScriptableObject> day5EmailSatisfaction;
    public List<EmailScriptableObject> day6EmailSatisfaction;
    public List<EmailScriptableObject> day7EmailSatisfaction;

    private Dictionary<int, List<EmailScriptableObject>> dayEmailMap;

    void Awake()
    {
        GameManager.OnSatisfactionChange += GameManagerOnSatisfactionChange;

        currentDay = GameManager.instance.day;

        dayEmailMap = new Dictionary<int, List<EmailScriptableObject>>
        {
            { 1, day1EmailSatisfaction },
            { 2, day2EmailSatisfaction },
            { 3, day3EmailSatisfaction },
            { 4, day4EmailSatisfaction },
            { 5, day5EmailSatisfaction },
            { 6, day6EmailSatisfaction },
            { 7, day7EmailSatisfaction }
        };
    }
    
    void GameManagerOnSatisfactionChange(int value, int satisfaction, int maxSatistafaction,int globalSatisfaction)
    {
        switch (satisfaction)
        {
            case 20:
                SendEmail(satisfaction);
                break;
            case 40:
                SendEmail(satisfaction);
                break;
            case 60:
                SendEmail(satisfaction);
                break;
            case 80:
                SendEmail(satisfaction);
                break;
            case 100:
                SendEmail(satisfaction);
                break;
            default:
                break;
        }
    }

    void SendEmail(int satisfaction)
    {
        if (dayEmailMap.TryGetValue(currentDay, out var emailList))
        {
            EmailScriptableObject email = GetEmailForSatisfaction(satisfaction, emailList);

            EmailController.instance.AddEmail(email);
        }
    }

    EmailScriptableObject GetEmailForSatisfaction(int satisfaction, List<EmailScriptableObject> emailList)
    {
        int index = (satisfaction / 20) - 1;
        if (index >= 0 && index < emailList.Count)
        {
            return emailList[index];
        }
        return null;
    }

    void OnDestroy()
    {
        GameManager.OnSatisfactionChange -= GameManagerOnSatisfactionChange;
    }
}

