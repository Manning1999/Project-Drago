using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Attack : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;
    private Animator anim;



    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        agent.isStopped = true;
      //  anim.SetBool("Attack", true);
        
    }


   


    public Attack(NPC _npc, NavMeshAgent _agent, Animator _anim)
    {
        agent = _agent;
        npc = _npc;
    }
}
