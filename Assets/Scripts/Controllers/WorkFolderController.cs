using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WorkFolderController : MonoBehaviour
{
    public GameObject selectGame;
    public GameObject workDone;
    public GameObject timesUpHero;
    public GameObject timesUpDiver;
    public bool timesUpScreen = false;

    void Awake()
    {
        GameManager.OnSatisfactionChange += GameManagerOnSatisfactionChange;
    }

    void Update()
    {
        if(timesUpHero.activeSelf || timesUpDiver.activeSelf){
            timesUpScreen = true;
        }else{
            timesUpScreen = false;
        }
    }

    void GameManagerOnSatisfactionChange(int value, int satisfaction, int maxSatisfaction, int globalSatisfaction)
    {
        if (timesUpScreen)
        {
            selectGame.SetActive(false);
            workDone.SetActive(false);
        }else if(satisfaction < 100 && !timesUpScreen){
            selectGame.SetActive(true);
            workDone.SetActive(false);
        }else{
            selectGame.SetActive(false);
            workDone.SetActive(true);
        }
    }

    void OnDestroy()
    {
        GameManager.OnSatisfactionChange -= GameManagerOnSatisfactionChange;
    }
}
