﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NPC : MonoBehaviour, IInteractable, IHurtable
{

    [SerializeField]
    protected string name = "";

    


    public enum NPCType { Enemy = 0, Ally = 1, ShopKeeper = 2, Guard = 3, Wanderer = 4}

    [SerializeField]
    public NPCType npcType = NPCType.Ally;

    protected bool isInteractable = true;
    public bool _isInteractable {get { return isInteractable; }  set { isInteractable = value; } }

    [SerializeField]
    protected int health = 100;

    [SerializeField]
    protected int maxHealth = 100;

    public int _health { get { return health; }  set { health = value; } }
    public int _maxHealth { get { return maxHealth; }  set { maxHealth = value; }  }


    [SerializeField]
    protected bool isEssential = false;
    public bool _isEssential { get { return isEssential; }  set { isEssential = value; } }

    protected bool isDead = false;
    public bool _isDead { get => isDead; set { isDead = value; } }

    [SerializeField]
    public UnityEvent OnInteract { get { return OnInteract; } }

    protected NavMeshAgent agent;



    public virtual void InteractWith()
    {
        //Do something when interacted with
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDie()
    {
       //Die
    }

    [ContextMenu("Take Damage")]
    [ExecuteInEditMode]
    public void DamageTest()
    {
        this.Damage(10);
    }



    public virtual void OnTakeDamage(int damageTaken)
    {

    }



    public void OnHeal(int healthRestored)
    {
        throw new System.NotImplementedException();
    }



    public void MoveToPosition(Vector3 positionToMoveTo)
    {
        agent.SetDestination(positionToMoveTo);
    }
}
