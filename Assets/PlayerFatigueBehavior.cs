using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFatigueBehavior : MonoBehaviour
{
    [Header("Settings")]

    [SerializeField] private float maxStamina;
    [SerializeField] private float currentStamina;

    public float MaxStamina { get { return maxStamina; } }
    public float CurrentStamina { get { return currentStamina; } }
    public float StaminaPercentage { get { return currentStamina / maxStamina; } }

    public bool autoRecovery = false;
    public bool isFatigued;
    public float timeToFullRecoveryAfterFatigue;
    public float staminaRecoverySpeed;

    private void Start()
    {
        currentStamina = maxStamina;
    }

    private void Update()
    {
        if (autoRecovery && !isFatigued)
            ChangeStamina(staminaRecoverySpeed * Time.deltaTime);
    }

    public void ChangeStamina(float stamina)
    {
        currentStamina += stamina;
        currentStamina = Mathf.Clamp(currentStamina, 0, maxStamina);

        if (!isFatigued && currentStamina == 0)
        {
            isFatigued = true;
            PlayerBehavior.ArmsAni.SetBool("fatigue", true);
            StartCoroutine(FullRecoveryAfterFatigue());
        }
    }

    IEnumerator FullRecoveryAfterFatigue()
    {
        yield return new WaitForSeconds(timeToFullRecoveryAfterFatigue);
        ChangeStamina(maxStamina);
        isFatigued = false;
        PlayerBehavior.ArmsAni.SetBool("fatigue", false);

    }
}
