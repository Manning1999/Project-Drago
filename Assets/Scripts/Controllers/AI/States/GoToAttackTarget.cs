using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToAttackTarget : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;
    private Animator anim;
    Vector3 targetPosition;


    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void Tick()
    {
        targetPosition = npc._attackTarget.transform.position;
        agent.SetDestination(targetPosition);
    }

    public GoToAttackTarget(NPC _npc, NavMeshAgent _agent, Animator _anim)
    {
        agent = _agent;
        npc = _npc;
       
    }
}
