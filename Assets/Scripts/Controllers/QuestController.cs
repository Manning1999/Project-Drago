using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    [SerializeField]
    private List<QuestLine> allQuests = new List<QuestLine>();


    List<QuestLine> availableQuests = new List<QuestLine>();

    
    protected QuestLine currentQuest = null;


    [SerializeField]
    protected GameObject questIcon = null;

    [SerializeField]
    Camera playerCamera = null;

    [SerializeField]
    private GameObject questList, questTitle, questDiscription;

    [SerializeField]
    protected GameObject questPrefab = null;

    // Start is called before the first frame update
    void Start()
    {
        AddQuest(allQuests[0]);
        currentQuest = availableQuests[0];
        UIController.Instance.UpdateQuestHUD(currentQuest);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentQuest != null)
        {
            questIcon.SetActive(true);
            GameObject target = currentQuest._currentObjective._target;

            if (target != null)
            {
                if (playerCamera.WorldToScreenPoint(target.transform.position).z >= 0)
                {
                    questIcon.transform.position = playerCamera.WorldToScreenPoint(target.transform.position);
                }
            }
            else
            {
                questIcon.SetActive(false);
            }
        }
        else
        {
            questIcon.SetActive(false);
        }
    }


    public void ShowQuestDetails(QuestLine questToShow)
    {

        string totalQuestInfo = questToShow._questOverview;

        foreach (QuestObjective objective in questToShow._questObjectives)
        {
            
            if(objective._showInMenu == true)
            {
                if(objective._isComplete == true)
                {
                    totalQuestInfo += "\n-X-" + objective._objectiveDescription + "-X-";
                }
                else
                {
                    totalQuestInfo += "\n\b-->" + objective._objectiveDescription + "<--";
                    break;
                }
            }
        }
    }

    /// <summary>
    /// Adds a quests to the quests log using a direct reference
    /// </summary>
    /// <param name="questToAdd"></param>
    public void AddQuest(QuestLine questToAdd)
    {
        availableQuests.Add(questToAdd);
    }


    /// <summary>
    /// Adds a quests using an index reference
    /// </summary>
    /// <param name="questToAdd"></param>
    public void AddQuest(int questToAdd)
    {
        availableQuests.Add(allQuests[questToAdd]);
    }

    [ContextMenu("Refresh Quest List")]
    public void RefreshQuestList()
    {
        
        foreach(QuestLine quest in availableQuests)
        {
            GameObject newQuest = Instantiate(questPrefab, questList.transform.position, Quaternion.identity, questList.transform);
            newQuest.transform.GetComponent<QuestLog>().SetDetails(quest._questTitle);
        }
    }
}
