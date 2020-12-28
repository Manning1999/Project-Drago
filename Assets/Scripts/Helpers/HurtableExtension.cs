using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HurtableExtension
{


    public static void Damage(this IHurtable iHurtable, int damageTaken)
    {

        if (iHurtable._isDead == false)
        {
            Debug.Log("Taken Damage");

            if (iHurtable._health <= 0)
            {
                // Debug.Break();
                if (iHurtable._isEssential == true)
                {

                }
                else
                {

                    iHurtable.Die();
                }
            }
            else
            {

                iHurtable._health -= damageTaken;
                iHurtable.OnTakeDamage(damageTaken);
            }
        }

       
  
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



    }

   


}
