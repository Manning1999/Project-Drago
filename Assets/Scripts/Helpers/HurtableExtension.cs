using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HurtableExtension
{


    public static void Damage(this IHurtable iHurtable, int damageTaken)
    {
        // All robots move based on their speed.
        iHurtable._health -= damageTaken;

        Debug.Log("Taken Damage");

        if(iHurtable._health <= 0)
        {
            if (iHurtable._isEssential == true)
            {
                
            }
            else
            {
                iHurtable.Die();
            }
        }

        iHurtable.OnTakeDamage(damageTaken);
  
    }



    public static void Heal(this IHurtable iHurtable, int healthRestored)
    {

        if (iHurtable._health + healthRestored >= iHurtable._maxHealth)
        {
            healthRestored -= healthRestored + iHurtable._health - iHurtable._maxHealth;
        }

        // Change the objects health
        iHurtable._health += healthRestored;

        iHurtable.OnHeal(healthRestored);

    }



    public static void Die(this IHurtable iHurtable)
    {

        Debug.Log("This object has died");

        iHurtable.Die();

    }



}
