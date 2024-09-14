using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public int satistafaction {get; private set; }
    public int maxSatistafaction {get; set; }
    public float stamina {get; private set;}
    public int maxStamina {get; private set;}
    public int health {get; private set;}
    public int maxHealth {get; private set;}
    public int day {get; private set; }
    public int moneyTotal {get; private set; }
    public int moneyToday {get; private set; }
    public int workScoreToday {get; private set; }
    public int workDoneToday {get; private set; }
    public static Action<int,int,int> OnSatisfactionChange;
    public static Action<float,float,int> OnStaminaChange;
    public static Action<int,int,int> OnMoneyChange;
    public static Action<int,int> OnWorkScoreTodayChange;
    public static Action<int,int> OnWorkDoneDayChange;
    public static Action<int> OnDayChange;
    public static Action<int,int,int> OnHealthChange;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        maxSatistafaction = 100;
        maxHealth = 100;
        day = 1;
    }

    public void SetSatisfaction(int value)
    {
        if(satistafaction < maxSatistafaction){
            satistafaction += value;
            OnSatisfactionChange?.Invoke(value,satistafaction,maxSatistafaction);
        }
    }
    public void SetHealth(int value)
    {
        if(health < maxHealth){
            health += value;
            OnHealthChange?.Invoke(value,health,maxHealth);
        }
    }
    public void ResetHeath()
    {
        health = maxHealth;
        OnHealthChange?.Invoke(0,health,maxHealth);
    }

    public void Stamina(float value)
    {
        stamina += value;
        OnStaminaChange?.Invoke(value,stamina,maxStamina);
    }
    public void SetMoneyTotal(int value)
    {
        moneyTotal += value;
        OnMoneyChange?.Invoke(value,moneyTotal,moneyToday);
    }

    public void SetMoneyToday(int value)
    {
        moneyToday += value;
        SetMoneyTotal(value);
    }

    public void SetWorkScoreToday(int value)
    {
        workScoreToday += value;
        OnWorkScoreTodayChange?.Invoke(value,workScoreToday);
    }
    public void SetWorkDoneToday(int value)
    {
        workDoneToday = value;
        OnWorkDoneDayChange?.Invoke(value,workDoneToday);
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
        OnSatisfactionChange?.Invoke(0,satistafaction,maxSatistafaction);
    }
    public void ResetWorkDoneToday()
    {
        workDoneToday = 0;
        OnWorkDoneDayChange?.Invoke(0,workDoneToday);
    }
    public void ResetWorkScoreToday()
    {
        workScoreToday = 0;
        OnWorkScoreTodayChange?.Invoke(0,workScoreToday);
    }    
    public void ResetMoneyToday()
    {
        moneyToday = 0;
        OnMoneyChange?.Invoke(0,moneyTotal,moneyToday);
    }
}

