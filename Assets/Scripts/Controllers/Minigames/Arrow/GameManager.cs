using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public BeatScroller theBS;

    public static GameManager instance;

    public float slidePerNote = 5f;
    public float slidePerGoodNote = 10f;
    public float slidePerPerfectNote = 15f;

    public int currentScore;
    public int scorePerNote = 50;
    public int scorePerGoodNote = 100;
    public int scorePerPerfectNote = 150;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public TMP_Text scoreText;
    public TMP_Text multiHit;

    public Slider slider;

    public GameObject _gameOver;
    public GameObject _workPanel;

    // Start is called before the first frame update
    void Start()
    {
        currentMultiplier = 1;
        scoreText.text = "0";
        slider.value = 100f;
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(theBS.hasStarted)
        {
            slider.value -= 0.1f; 
            if(slider.value == 0)
            {
                GameOver();
            }
        }
        
    }

    public void GameOver()
    {
        theBS.hasStarted = false;
        _gameOver.SetActive(true);
        _workPanel.SetActive(false);
    }

    public void StartWorking()
    {
        theBS.hasStarted = true;
        slider.value = 100f;
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time!");

        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        
        multiHit.text = "x" + currentMultiplier;

        // currentScore += scorePerNote * currentMultiplier;
        slider.value += slidePerNote;
        scoreText.text = currentScore.ToString();


    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteMissed()
    {
        Debug.Log("Missed!");
        slider.value -= slidePerNote;
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiHit.text = "x" + currentMultiplier;
    }
}
