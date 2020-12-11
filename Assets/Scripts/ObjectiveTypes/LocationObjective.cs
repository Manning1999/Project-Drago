using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LocationObjective : QuestObjective
{

    [SerializeField]
    protected UnityEvent OnEnterArea = new UnityEvent();

    [SerializeField]
    protected UnityEvent OnExitArea = new UnityEvent();

    [SerializeField]
    [Tooltip("If true, the objective will be complete when the player enters the area. If false, the objective will be complete when the player leaves the area")]
    protected bool completeOnEnter = true;

    protected void OnTriggerEnter(Collider col)
    {
        if (col.transform.GetComponent<PlayerController>() != null)
        {
            OnEnterArea.Invoke();
            if (completeOnEnter == true)
            {

                CompleteObjective();
            }
        }
        

    }


    protected void OnTriggerExit(Collider col)
    {
        if (col.transform.GetComponent<PlayerController>() != null)
        {
            OnExitArea.Invoke();
            if (completeOnEnter == false)
            {
                
                CompleteObjective();
            }
        }


    }
}
