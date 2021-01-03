using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToPlayer : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;

    public void OnEnter()
    {
       

    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        agent.SetDestination(PlayerController.Instance.transform.position);
    }

    public GoToPlayer(NPC _npc, NavMeshAgent _agent)
    {
        agent = _agent;
        npc = _npc;
    }
}
