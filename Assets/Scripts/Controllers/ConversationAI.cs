using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationAI : NPC
{

    UIController uiCont = null;

    [SerializeField]
    protected bool isTalkable = true;

    [SerializeField]
    [Tooltip("If true, the 'Conversation Options' menu will show, if false, the Ai will just make some sort of passing remark when interacted with")]
    protected bool requiresFocus = true;

    [SerializeField]
    protected List<ConversationElement> conversationLines = new List<ConversationElement>();
    public List<ConversationElement> _conversationLines { get { return conversationLines; } }

    // Start is called before the first frame update
    void Start()
    {
        uiCont = UIController.Instance;
        foreach(ConversationElement element in conversationLines)
        {
            element.SetParent(this);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void InteractWith()
    {
        if(isTalkable == true)
        {
            if(requiresFocus == true)
            {
                uiCont.ShowConversationOptions(conversationLines);
            }
            else
            {
                
            }
        }
        else
        {

        }
    }


    /// <summary>
    /// This sets the isTalkable variable which controls whether or not the AI can be spoken to
    /// </summary>
    /// <param name="set"></param>
    public void SetCanTalk(bool set)
    {
        isTalkable = set;   
    }


}
