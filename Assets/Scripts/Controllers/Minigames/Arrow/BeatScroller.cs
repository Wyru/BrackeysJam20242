using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class BeatScroller : MonoBehaviour
{
    public static BeatScroller instance;
    public float beatTempo = 120;
    public float beatTempoBase = 120;
    public bool hasStarted;

    public GameObject[] _lanes;
    public GameObject[] _arrows;

    public Timer _timer;
    public Timer _timerTempoTotal;
    public GameObject _closeWindows;

    public TMP_Text _timeupTimer;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        if(!hasStarted){
            return;
        }else
        {   
            if(_timer.Timeout){
                int laneNumber = Random.Range(0,4);
                Instantiate(_arrows[laneNumber], _lanes[laneNumber].gameObject.transform.GetChild(0).transform.position, Quaternion.identity);
                _timer.StartTimer();
            }
            
            _timeupTimer.text = Mathf.RoundToInt(_timerTempoTotal.secondsCounter).ToString();
        }

        if(_timerTempoTotal.Timeout)
        {
            CleanUp();
            ArrowGameManager.instance.TimeUp();
        }
    }

    public void CleanUp()
    {
        hasStarted = false;
        ArrowGameManager.instance.DestroyAllClones();
        _closeWindows.SetActive(true);
        beatTempo = 120;
        beatTempoBase = 120;
    }

    public void SetVariables(){
        _closeWindows.SetActive(false);
        hasStarted = true;
        beatTempo = beatTempo / 60;
        beatTempoBase = beatTempo;
        _timer.StartTimer();
        _timerTempoTotal.StartTimer();
    }

    public void GetHarder()
    {
        beatTempo += beatTempoBase * 0.15f;
        _timer.seconds *= 0.815f; 
    }


    
}
