using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DragonTakeOff : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;
    private Animator anim;
    Vector3 targetPosition;
    Rigidbody rb;


    public void OnEnter()
    {
        agent.enabled = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        //Play take off animation

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        
    }

    public DragonTakeOff(NPC _npc, NavMeshAgent _agent, Animator _anim, Rigidbody _rb)
    {
        agent = _agent;
        npc = _npc;
        rb = _rb;

    }
}
