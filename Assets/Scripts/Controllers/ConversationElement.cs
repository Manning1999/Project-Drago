using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ConversationElement : MonoBehaviour
{

    UIController uiCont = null;

    [System.Serializable]
    public class ConversationLine
    {
        public string speech;
        public int time;
    }

    ConversationAI parent = null;

    [SerializeField]
    bool isRepeatable = true;
    public bool _isRepeatable { get { return isRepeatable; } }

    [SerializeField]
    bool isAvailable = true;
    public bool _isAvailable { get { return isAvailable; } }

    [SerializeField]
    bool hasBeenUsed = false;
    public bool _hasBeenUsed { get { return hasBeenUsed; } }

    [SerializeField]
    [Tooltip("These are conversations that will be unlocked after this one")]
    List<ConversationElement> unlockConversations = new List<ConversationElement>();

    [SerializeField]
    [Tooltip("These are conversations that must be unlocked before this one can be used")]
    List<ConversationElement> prerequisiteConversations = new List<ConversationElement>();

    [SerializeField]
    public string playerSpeech = "";

    [SerializeField]
    protected List<ConversationLine> aiResponses = new List<ConversationLine>();


    [SerializeField]
    protected UnityEvent OnStart = new UnityEvent();


    [SerializeField]
    protected UnityEvent OnComplete = new UnityEvent();


    [SerializeField]
    protected bool closesConversation = false;

    protected void Start()
    {
        uiCont = UIController.Instance;
    }



    public void TriggerConversation()
    {
        StartCoroutine(AIResponseTimer());
        OnStart.Invoke();
        hasBeenUsed = true;

        //If the speech option is not available any more then remove it
        if (isRepeatable == false)
        {
            isAvailable = false;
            if (closesConversation == false)
            {
                uiCont.ShowConversationOptions(parent._conversationLines, true);
            }

        }

        
        if (closesConversation == true)
        {
            uiCont.ShowUIElement(null);
        }

    }

    /// <summary>
    /// This controls how long things are said for
    /// </summary>
    /// <returns></returns>
    public IEnumerator AIResponseTimer()
    {
        foreach (ConversationLine line in aiResponses) {

            
            uiCont.SetFocusedSpeech(line.speech);
            yield return new WaitForSeconds(line.time);
        }
        uiCont.SetFocusedSpeech("");
        OnComplete.Invoke();

       

    }


    public IEnumerator AIResponseTimer(ConversationLine line)
    {
        uiCont.SetFocusedSpeech(line.speech);
        yield return new WaitForSeconds(line.time);
        OnComplete.Invoke();

        if (isRepeatable == true)
        {
            hasBeenUsed = true;
        }
    }


    /// <summary>
    /// This will pick a random response 
    /// </summary>
    public virtual void ChooseRandomResponse()
    {
        //uiCont.SetSpeechSubtitle(aiResponses[Random.Range(0, aiResponses.Count)].speech);
    }


    public void SetAvailability(bool set)
    {
        isAvailable = set;
    }


    public void SetParent(ConversationAI ai)
    {
        parent = ai;
    }

   




}
