using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewEmail", menuName = "Email System/Email")]
public class EmailScriptableObject : ScriptableObject
{
    public string emailTitle;   // The title of the email
    public string emailContent; // The content of the email
    public string sender;
}
