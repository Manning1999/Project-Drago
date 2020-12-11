using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightObjective : QuestObjective
{

    [SerializeField]
    protected List<GameObject> objectsToHighlight = new List<GameObject>();

    public override void StartObjective()
    {
        base.StartObjective();

    }


}
