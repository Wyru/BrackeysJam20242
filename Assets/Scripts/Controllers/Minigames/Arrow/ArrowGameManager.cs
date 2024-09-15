using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ArrowGameManager : MonoBehaviour
{
    public BeatScroller theBS;
    public static ArrowGameManager instance;
    
    private float slidePerNoteMiss = 5f;
    private float slidePerNote = 3f;
    private float slidePerGoodNote = 5f;
    private float slidePerPerfectNote = 8f;

    public int finalScoreSave;
    private int currentScore;
    private int scorePerNote = 50;
    private int scorePerGoodNote = 100;
    private int scorePerPerfectNote = 150;
    private int maxCombo;
    
    public int timeWorked = 0;
    public int rankPosition = 1785;
    public int costForCompany = 45789;
    public int moneyTotal;
    public int moneyPerWork;

    private int currentMultiplier;
    private int multiplierTracker;

    [Header("Combo Thresholds")]
    public int[] multiplierThresholds;

    [Header("Reference Texts On game")]
    public TMP_Text scoreText;
    public TMP_Text multiHit;
    public TMP_Text endScore;
    public TMP_Text endPositionRank;
    public TMP_Text companyCostText;
    public TMP_Text _currentHitText;
    public Slider slider;

    [Header("Reference for Objecs")]
    public GameObject _gameOver;
    public GameObject _workPanel;
    public GameObject _chooseWork;
    public GameObject _timeUp;
    public GameObject _goodEmployer;
    public GameObject _badEmployer;
    private GameManager _gameManager;

    private string _perfectText = "Shareholders are happy :)";
    private string _goodText = "Need Improve";
    private string _normalText = "Lack of Softskills";
    private string _missedText = "Quiet Vacation";

    // Start is called before the first frame update
    void Start()
    {
        currentMultiplier = 1;
        scoreText.text = "0";
        instance = this;
        maxCombo = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if(theBS.hasStarted)
        {
            slider.value -= 0.1f * Mathf.Max(currentScore / 10000, 1); 
            if(slider.value == 0)
            {
                GameOver();
            }
        }
        
    }

    public void GameOver()
    {
        theBS.hasStarted = false;
        theBS.CleanUp();
        _gameOver.SetActive(true);
        _workPanel.SetActive(false);
        _timeUp.SetActive(false);
    }

    public void TimeUp()
    {
        theBS.hasStarted = false;
        _timeUp.SetActive(true);
        _gameOver.SetActive(false);
        _chooseWork.SetActive(false);
        _workPanel.SetActive(false);
        moneyPerWork = currentScore / 500;
        finalScoreSave += currentScore;
        if(currentScore == 0){
            _goodEmployer.SetActive(false);
            _badEmployer.SetActive(true);
        }else{
            _badEmployer.SetActive(false);
            _goodEmployer.SetActive(true);
        }
        endScore.text = "Score: " + currentScore.ToString();
        SetAllVariables();
        CleanUp();
    }

    private void CleanUp()
    {
        currentScore = 0;
        finalScoreSave = 0;
        currentMultiplier = 1;
        scoreText.text = "0";
        slider.value = 100f;
        instance = this;
        maxCombo = 1;
        multiplierTracker = 0;
        moneyPerWork = 0;
    }

    public void StartWorking()
    {
        theBS.hasStarted = true;
        slider.value = 100f;
    }

    public void NoteHit()
    {
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker++;
            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                if(currentMultiplier > maxCombo){
                    maxCombo = currentMultiplier;
                    theBS.GetHarder();
                }
                multiplierTracker = 0;
                currentMultiplier++;
            }
        }
        
        multiHit.text = "x" + currentMultiplier;

        
        scoreText.text = currentScore.ToString();
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        slider.value += slidePerNote * (currentMultiplier * 0.75f);
        NoteHit();
        HitText(_normalText);
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        slider.value += slidePerGoodNote * (currentMultiplier * 0.75f);
        NoteHit();
        HitText(_goodText);
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        slider.value += slidePerPerfectNote* (currentMultiplier * 0.75f);
        NoteHit();
        HitText(_perfectText);
    }

    private void HitText(string text)
    {
        _currentHitText.text = text;
    }

    public void NoteMissed()
    {
        slider.value -= slidePerNoteMiss;
        HitText(_missedText);
        currentMultiplier = 1;
        multiplierTracker = 0;
        multiHit.text = "x" + currentMultiplier;
    }

    public void DestroyAllClones()
    {
        GameObject[] arrows = GameObject.FindGameObjectsWithTag("Arrows");

        foreach (GameObject arrow in arrows)
        {
            Destroy(arrow);
        }
    }

    public void SetAllVariables()
    {
        if(finalScoreSave > 0){
            timeWorked++;

            _gameManager.SetSatisfaction(10);
            _gameManager.SetWorkScoreToday(finalScoreSave);
            _gameManager.SetWorkDoneToday(timeWorked);
            _gameManager.SetMoneyToday(moneyPerWork);
            companyCostText.text = "Your cost for the company: $" + (costForCompany + moneyPerWork);
            if(timeWorked > 1){
                rankPosition = rankPosition + timeWorked * Random.Range(1,100);
                endPositionRank.text = rankPosition.ToString();
            }else{
                endPositionRank.text = rankPosition.ToString();
            }
        }else{
            _gameManager.SetWorkScoreToday(finalScoreSave);
            _gameManager.SetWorkDoneToday(timeWorked);
            _gameManager.SetMoneyToday(moneyPerWork);
        }
    }

    void OnEnable()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    void OnDisable()
    {
        _gameManager = null;
    }
}
