using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConversationButton : MonoBehaviour
{

    protected ConversationElement line= null;

    [SerializeField]
    private TextMeshProUGUI text = null;

    

    public void TriggerConversation() { 

        line.TriggerConversation();
        
    }

    public void SetConversationDetails(ConversationElement element)
    {
        line = element;
        text.text = line.playerSpeech;
    }
}
