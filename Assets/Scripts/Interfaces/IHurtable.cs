using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHurtable
{
    bool _isEssential { get; set; }
    int _health { get; set; }
    int _maxHealth { get; set; }


    void OnTakeDamage(int damageTaken);
   

    void OnHeal(int healthRestored);

    void OnDie();
    
}
