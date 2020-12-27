using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWord : MonoBehaviour
{

    [SerializeField]
    protected string word;
    public string _word {  get { return word; } protected set { word = value; } }

    [SerializeField]
    protected bool canDraw;
    public bool _canDraw { get { return canDraw; } protected set { canDraw = value; } }

    [SerializeField]
    protected bool requiresObject = true;
    public bool _requiresObject { get { return requiresObject; } protected set { requiresObject = value; } }

    [SerializeField]
    [Tooltip("If true, the line will be straightened to stretch from it's start point to end point. I.e. it will become a straight line instead of a squigly line")]
    private bool doesDrawStartToEnd = false;
    public bool _doesDrawStartToEnd { get { return doesDrawStartToEnd; } protected set { doesDrawStartToEnd = value; } }

    [SerializeField]
    [Tooltip("If true, the starting point of a line needs to be from a magical object")]
    protected bool requiresStartOnMagicObject = false;
    public bool _requiresStartOnMagicObject { get { return requiresStartOnMagicObject; } protected set { requiresStartOnMagicObject = value; } }

    [SerializeField]
    [Tooltip("If true, the player will be able to draw multiple lines while doing magic with this spell")]
    protected bool canDrawMultipleLines = false;
    public bool _canDrawMultipleLines { get { return canDrawMultipleLines; } protected set { canDrawMultipleLines = value; } }

    [SerializeField]
    protected int baseManaUsage = 5;

    [SerializeField]
    protected List<MagicUpgrades> upgrades = new List<MagicUpgrades>();





    public virtual void Activate(GameObject objectOfSentence = null, MagicAdverb adjective = null, Vector3[] locations = null)
    {
        
       
    }

    [ContextMenu("Learn word")]
    public virtual void LearnWord()
    {
        MagicController.Instance.LearnMagicWord(this);
        NotificationController.Instance.CreateNotification("Learnt new word:", word);
    }
    
}
