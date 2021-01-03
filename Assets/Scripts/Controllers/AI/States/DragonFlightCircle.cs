using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFlightCircle : IState
{
                            
    private readonly NPC npc;
    private Animator anim;
    Vector3 targetPosition;
    Rigidbody rb;


    public void OnEnter()
    {
       
    }

    public void OnExit()
    {

    }

    public void Tick()
    {

    }

    

    public DragonFlightCircle(NPC _npc, Animator _anim, Rigidbody _rb)
    {

        npc = _npc;
        rb = _rb;

    }
}
