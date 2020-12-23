using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{

    protected bool isSelected = false;
    public bool _isSelected { get { return isSelected; } }

    [SerializeField]
    private TextMeshProUGUI textReference = null;

    [SerializeField]
    protected Color selectedColor = Color.gray;
    protected Color originalColor;

    protected Image imageComponent = null;

    private QuestLine associatedQuest = null;

    protected void Start()
    {
        imageComponent = transform.GetComponent<Image>();
        originalColor = imageComponent.color;

    }

    public void SetSelected(bool set)
    {
        isSelected = set;

        if(set == true)
        {
            
            imageComponent.color = selectedColor;
            QuestController.Instance.ShowQuestDetails(associatedQuest);
        }
        else
        {
            imageComponent.color = originalColor;
        }
    }

    public void Press()
    {
        SetSelected(!isSelected);
    }

    public void SetDetails(string title, QuestLine quest)
    {
        textReference.text = title;
        associatedQuest = quest;
    }
}
