using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtable
{

    void TakeDamage(int damageTaken);

    void Heal(int healthRestored);

    void Die();
    
}
