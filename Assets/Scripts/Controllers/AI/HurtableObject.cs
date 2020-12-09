using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtableObject : MonoBehaviour, IHurtable
{
    [SerializeField]
    int maxHealth = 100;

    [SerializeField]
    int currentHealth = 100;


    public void Heal(int healthRestored)
    {
        //If too much health is restored then prevent it from going over the max
        if (currentHealth + healthRestored > maxHealth)
        {
            healthRestored = currentHealth + healthRestored - maxHealth;
        }

        currentHealth += healthRestored;
    }


    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0) Die();
    }

    public virtual void Die()
    {
        Debug.Log("This character has died");
    }


   
}
