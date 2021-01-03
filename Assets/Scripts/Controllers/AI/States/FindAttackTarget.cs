using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class FindAttackTarget : IState
{

    NavMeshAgent agent;

    private readonly NPC npc;
    private Animator anim;
    private NPC.NPCType targetType;
    List<NPC> enemiesInRange;

    public void OnEnter()
    {
        npc.SetAttackTarget(DecideTarget());
        agent.ResetPath();
    }

    public void OnExit()
    {

    }

    public void Tick()
    {
          
    }


    private NPC DecideTarget()
    {
            Debug.Log(enemiesInRange.Count);
            return enemiesInRange.OrderBy(t => Vector3.Distance(npc.transform.position, t.transform.position)).First();
            //.Where(t => t._health >= 1)
           // .Take(2)
           // .OrderBy(t => Random.Range(0, int.MaxValue))
           // .FirstOrDefault();

            

    }

    public FindAttackTarget(NPC _npc, NavMeshAgent _agent, List<NPC> _enemiesInRange)
    {                                                                                         
        agent = _agent;
        npc = _npc;
        enemiesInRange = _enemiesInRange;
    }
}
