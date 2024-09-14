using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public int satistafaction;
    public int maxSatistafaction;
    public float stamina;
    public int maxStamina;
    public int health;
    public int maxHealth = 100;
    public int day;
    public int moneyTotal;
    public int moneyToday;
    public int workScoreToday;
    public int workDoneToday;
    public static Action<int, int, int> OnSatisfactionChange;
    public static Action<float, float, int> OnStaminaChange;
    public static Action<int, int, int> OnMoneyChange;
    public static Action<int, int> OnWorkScoreTodayChange;
    public static Action<int, int> OnWorkDoneDayChange;
    public int globalSatisfaction {get; set; }
    public static Action<int> OnDayChange;
    public static Action<int, int, int> OnHealthChange;

    void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
        health = maxHealth;
        maxSatistafaction = 110;
        day = 1;
    }

    public void SetSatisfaction(int value)
    {
        if(satistafactionToday < maxSatistafaction){
            satistafactionToday += value;
            SetGlobalSatisfaction(value);
            OnSatisfactionChange?.Invoke(value,satistafactionToday,maxSatistafaction,globalSatisfaction);
        }
    }
    public void SetHealth(int value)
    {
        if (health <= maxHealth)
        {
            health += value;
            OnHealthChange?.Invoke(value, health, maxHealth);
        }
    }

    public void SetGlobalSatisfaction(int value)
    {
        globalSatisfaction += value;
    }
    public void ResetHeath()
    {
        health = maxHealth;
        OnHealthChange?.Invoke(0, health, maxHealth);
    }

    public void Stamina(float value)
    {
        stamina += value;
        OnStaminaChange?.Invoke(value, stamina, maxStamina);
    }
    public void SetMoneyTotal(int value)
    {
        moneyTotal += value;
        OnMoneyChange?.Invoke(value, moneyTotal, moneyToday);
    }

    public void SetMoneyToday(int value)
    {
        moneyToday += value;
        SetMoneyTotal(value);
    }

    public void SetWorkScoreToday(int value)
    {
        workScoreToday += value;
        OnWorkScoreTodayChange?.Invoke(value, workScoreToday);
    }
    public void SetWorkDoneToday(int value)
    {
        workDoneToday = value;
        OnWorkDoneDayChange?.Invoke(value, workDoneToday);
    }

    public void IncrementDay()
    {
        day++;
        OnDayChange?.Invoke(day);
        ResetSatisfaction();
        ResetWorkDoneToday();
        ResetWorkScoreToday();
        ResetMoneyToday();
    }

    public void ResetSatisfaction()
    {
        satistafaction = 0;
        OnSatisfactionChange?.Invoke(0, satistafaction, maxSatistafaction);
        satistafactionToday = 0;
        OnSatisfactionChange?.Invoke(0,satistafactionToday,maxSatistafaction,globalSatisfaction);
    }
    public void ResetWorkDoneToday()
    {
        workDoneToday = 0;
        OnWorkDoneDayChange?.Invoke(0, workDoneToday);
    }
    public void ResetWorkScoreToday()
    {
        workScoreToday = 0;
        OnWorkScoreTodayChange?.Invoke(0, workScoreToday);
    }
    public void ResetMoneyToday()
    {
        moneyToday = 0;
        OnMoneyChange?.Invoke(0, moneyTotal, moneyToday);
    }
}

