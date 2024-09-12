using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SequencyToPress : MonoBehaviour
{
    public static SequencyToPress instance;
    private List<GameObject> _sequence = new List<GameObject>();
    private List<GameObject> _spawnedObjects = new List<GameObject>();

    public List<GameObject> _whatToSpawn;
    public List<GameObject> _playerInputs;
    public List<GameObject> _areaToSpawn;

    private int scorePoints;
    private int pointsForComplete = 1500;
    private int pointsPerNote = 150;
    private int removePoints = -50;

    [Header("Texts")]
    public TMP_Text scoreText;
    public TMP_Text timerText;
    public TMP_Text finalScore;
    public TMP_Text finalRank;

    public GameObject _endGame;
    public GameObject _workPanel;
    public GameObject _closeWindows;

    private int currentInputIndex = 0;
    public Timer timer;

    private bool gameHasStarted;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void StartGame()
    {
        _closeWindows.SetActive(false);
        GenerateRandomSequence();
        SpawnObjects();
        scorePoints = 0;
        timer.StartTimer();
        gameHasStarted = true;
    }

    private void ResetGame()
    {
        DestroyArrowOnScreen();
        GenerateRandomSequence();
        SpawnObjects();
    }

    void Update()
    {
        if(!gameHasStarted) return;
        if(scorePoints >= 0){
            scoreText.text = scorePoints.ToString();
        }
        
        timerText.text = Mathf.RoundToInt(timer.secondsCounter).ToString();

        if(timer.Timeout)
        {
            SetPointsFinal();
        }
    }
    
    void GenerateRandomSequence()
    {
        _sequence.Clear();
        _spawnedObjects.Clear();

        for (int i = 0; i < _whatToSpawn.Count; i++)
        {
            int randomIndex = Random.Range(0, _whatToSpawn.Count);
            _sequence.Add(_whatToSpawn[randomIndex]);
        }
    }

    void SpawnObjects()
    {
        for (int i = 0; i < _whatToSpawn.Count; i++)
        {
            GameObject clone = Instantiate(_sequence[i], _areaToSpawn[i].transform.position, Quaternion.identity);
            clone.transform.SetParent(gameObject.transform);
            _spawnedObjects.Add(clone);
        }
    }

    public void CheckPlayerInput(GameObject selectedObject)
    {
        if (selectedObject.GetComponent<HellDiverPlayerArrows>().keyToPress == _sequence[currentInputIndex].GetComponent<HellDiverArrow>().keyToPress)
        {
            _spawnedObjects[currentInputIndex].GetComponent<SpriteRenderer>().color = Color.yellow;
            GivePoints(pointsPerNote);
            currentInputIndex++;
            if (currentInputIndex >= _sequence.Count)
            {
                SequenceCompleted();
            }
        }
        else
        {
            GivePoints(removePoints);
            _spawnedObjects[currentInputIndex].GetComponent<SpriteRenderer>().color = Color.red;
            ResetSequence();
        }
        
        void ResetSequence()
        {
            currentInputIndex = 0;
            ResetGame();
        }

        void SequenceCompleted()
        {
            GivePoints(pointsForComplete);
            currentInputIndex = 0;
            ResetGame();
        }
    }

    void DestroyArrowOnScreen()
    {
        for (int i = 0; i < _spawnedObjects.Count; i++)
        {
            Destroy(_spawnedObjects[i]);
        }
    }

    void GivePoints(int giveScore)
    {
        if(scorePoints >= 0){
            scorePoints += giveScore;
        }else if(scorePoints < 0) scorePoints = 0;
        
    }

    void SetPointsFinal()
    {

        gameHasStarted = false;
        _endGame.SetActive(true);
        _closeWindows.SetActive(true);
        _workPanel.SetActive(false);
        GameManager manager = GameManager.instance;
        finalScore.text = scorePoints.ToString();
        manager.finalScoreSave += scorePoints;
        manager.timeWorked++;
        manager.SetAllVariables();
        finalRank.text = manager.rankPosition.ToString();
    }

}
