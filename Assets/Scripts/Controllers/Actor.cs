using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Actor : MonoBehaviour
{

    public UnityEvent OnDeath;
    public UnityEvent OnTakeDamage;
    public bool destroyOnDeath;
    public int currentHealth;
    public int maxHealth;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int amount)
    {
        OnTakeDamage?.Invoke();
        currentHealth -= amount;
        if (currentHealth <= 0)
        { Death(); }
    }

    void Death()
    {
        // Death function
        // TEMPORARY: Destroy Object
        OnDeath?.Invoke();
        if (destroyOnDeath)
            Destroy(gameObject);
    }

}
