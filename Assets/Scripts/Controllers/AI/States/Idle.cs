using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Idle : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;
    private Animator anim;

    public void OnEnter()
    {
        if (agent.isOnNavMesh == true)
        {
           // agent.isStopped = true;
            agent.ResetPath();
        }
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
                   
    }

    public Idle(NPC _npc, NavMeshAgent _agent, Animator _anim)
    {
        agent = _agent;
        npc = _npc;
    }
}
