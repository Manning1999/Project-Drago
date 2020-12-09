using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestObjective : MonoBehaviour
{
    [SerializeField]
    private QuestLine questLine = null;

    public UnityEvent OnCompleteObjective = new UnityEvent();    
    public UnityEvent OnStartObjective = new UnityEvent();    

    [SerializeField]
    protected GameObject target = null;

    public GameObject _target { get { return target; } }

    [SerializeField]
    protected string objectiveDesciption, objectiveTitle;


    public string _objectiveDescription { get { return objectiveDesciption; } }
    public string _objectiveTitle { get { return objectiveTitle; } }

    [SerializeField]
    protected bool isComplete = false;
    public bool _isComplete {  get { return isComplete; } }

    protected bool showInMenu = true;
    public bool _showInMenu { get { return showInMenu; } }



    public virtual void CompleteObjective()
    {
        if (isComplete == false)
        {
            OnCompleteObjective.Invoke();
            isComplete = true;
            questLine.CompleteObjective();
        }
    }

    public virtual void StartObjective()
    {
        //Do what should happen at the start of this objective e.g. highlight the object
        OnStartObjective.Invoke();
    }

    public void SetQuest(QuestLine quest)
    {
        questLine = quest;
    }
}
