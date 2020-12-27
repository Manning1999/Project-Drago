using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestLine : MonoBehaviour
{

    [SerializeField]
    protected string questTitle;
    public string _questTitle { get { return questTitle; } }

    [SerializeField]
    protected List<QuestObjective> questObjectives = new List<QuestObjective>();
    public List<QuestObjective> _questObjectives { get { return questObjectives; } }

    [SerializeField]
    protected QuestObjective currentObjective = null;


    [SerializeField]
    protected int reachedObjective = 0;

    public QuestObjective _currentObjective { get { return currentObjective; } }

    public UnityEvent OnCompleteQuest = new UnityEvent();

    [SerializeField]
    [TextArea(1, 7)]
    protected string questOverview = "";
    public string _questOverview { get { return questOverview; } }

    [SerializeField]
    protected int questExpReward = 0;
    public int _questExpReward { get { return questExpReward; } }


    public QuestLine(string title)
    {
        questTitle = title;
    }

    public void Start()
    {
        foreach(QuestObjective objective in questObjectives)
        {
            objective.SetQuest(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CompleteObjective()
    {
        reachedObjective++;

        if (reachedObjective == questObjectives.Count)
        {
            CompleteQuest();
            return;
        }


        currentObjective = questObjectives[reachedObjective];
        currentObjective.StartObjective();
        UIController.Instance.UpdateQuestHUD(this);

        
    }

    public void CompleteQuest()
    {
        OnCompleteQuest.Invoke();
        UIController.Instance.UpdateQuestHUD(null);
        NotificationController.Instance.CreateNotification("Finished Quest:", questTitle);
    }

    

}
