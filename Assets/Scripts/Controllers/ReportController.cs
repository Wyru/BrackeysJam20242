using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ReportController : MonoBehaviour
{
    public TMP_Text satisfactionText;
    public TMP_Text workDoneText;
    public TMP_Text moneyEarnText;
    public TMP_Text recordScoreText;

    void Awake()
    {
        GameManager.OnSatisfactionChange += GameManagerOnSatisfactionChange;
        GameManager.OnWorkScoreTodayChange += GameManagerOnworkScoreGlobalChange;
        GameManager.OnWorkDoneDayChange += GameManagerOnWorkDoneDayChange;
        GameManager.OnMoneyChange += GameManagerOnMoneyChange;
    }

    void GameManagerOnSatisfactionChange(int value, int satisfaction, int maxSatistafaction)
    {
        satisfactionText.text = "Company satisfaction: " + satisfaction.ToString() + "%";
    }

    void GameManagerOnworkScoreGlobalChange(int value, int workScore)
    {
        recordScoreText.text = workScore.ToString();
    }
    void GameManagerOnWorkDoneDayChange(int value, int workDone)
    {
        workDoneText.text = "Work done today: " + workDone.ToString();
    }

    void GameManagerOnMoneyChange(int value, int moneyPerWork,int moneyToday)
    {
        moneyEarnText.text = "Money to receive today: $" + moneyToday.ToString();
    }

    void OnDestroy(){
        GameManager.OnSatisfactionChange -= GameManagerOnSatisfactionChange;
        GameManager.OnWorkScoreTodayChange -= GameManagerOnworkScoreGlobalChange;
        GameManager.OnWorkDoneDayChange -= GameManagerOnWorkDoneDayChange;
        GameManager.OnMoneyChange -= GameManagerOnMoneyChange;
    }
}
