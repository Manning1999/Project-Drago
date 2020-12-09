﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{

    //create singleton
    public static UIController instance;
    private static UIController _instance;

    public static UIController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<UIController>();
            }

            return _instance;
        }
    }

    protected bool isPaused = false;

    public DragonController playerDragon = null;

    [SerializeField]
    protected TextMeshProUGUI dragonNameInput = null;

    [SerializeField]
    protected GameObject dragonNameInputUI = null;

    [SerializeField]
    private GameObject questLogUI = null;

    [SerializeField]
    private GameObject pauseMenu = null;

    [SerializeField]
    private TextMeshProUGUI questHUD = null;

    [SerializeField]
    private List<GameObject> togglableUIElements = new List<GameObject>();

    [SerializeField]
    protected GameObject conversationUI, conversationOptionsContent;

    [SerializeField]
    protected TextMeshProUGUI focusedSpeech = null;

    protected List<GameObject> activePlayerSpeechOptions = new List<GameObject>();
    protected List<GameObject> inactivePlayerSpeechOptions = new List<GameObject>();



    protected bool isInConversation = false;

    [SerializeField]
    protected GameObject speechPrefab = null;

    protected List<GameObject> InactiveSpeechObjects = new List<GameObject>();
    protected List<GameObject> activeSpeechObjects = new List<GameObject>();



    public void Start()
    {
        ShowUIElement(null);
        SetMouseLock(true);
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Hide conversation Menu
            if(isInConversation == true)
            {
                //Don't do anything. People need to say goodbye before leaving. Not saying goodbye is just rude...
            }
            else
            {
                if (isPaused == true)
                {
                    //Hide pause menu
                }

                RaycastInteractionController.Instance.SetCanInteract(true);
                ShowUIElement(null);
            }
            
           
        }
    }


    /// <summary>
    /// This will show a specific togglable UI element and hide all others. A null or invalid gameobject will hide all togglable UI elements
    /// </summary>
    /// <param name="uiElementToShow"></param>
    public void ShowUIElement(GameObject uiElementToShow)
    {

        if (uiElementToShow != null && togglableUIElements.Contains(uiElementToShow))
        {
            PlayerController.Instance.SetCanLook(false);
            SetMouseLock(false);
            RaycastInteractionController.Instance.SetCanInteract(false);
        }
        else
        {
            PlayerController.Instance.SetCanLook(true);
            SetMouseLock(true);
            RaycastInteractionController.Instance.SetCanInteract(true);

           
            isInConversation = false;
            focusedSpeech.gameObject.SetActive(false);
            ClearPlayerSpeechOptions();
            focusedSpeech.SetText("");
            Debug.Log("Hiding everything now");

        }

        foreach (GameObject UIElement in togglableUIElements)
        {
            if(UIElement != uiElementToShow)
            {
                UIElement.SetActive(false);
                
            }
            else
            {
                UIElement.SetActive(true);
                Debug.Log("Showing something");
            }
        }
    }


    public void SetTimeScale(float time)
    {
        Time.timeScale = time;
    }



    public void SetDragonName()
    {
        ShowUIElement(null);
        SetMouseLock(true);
        PlayerController.Instance.SetCanLook(true);
        SetTimeScale(1);
        playerDragon.SetName(dragonNameInput.text);
        
    }



    public void UpdateQuestHUD(QuestLine quest)
    {
        if (quest == null)
        {
            questHUD.text = "";
            return;
        }
        questHUD.text = quest._questTitle + "\n" + quest._currentObjective._objectiveTitle;
    }


    /// <summary>
    /// True = mouse is locked
    /// False = mouse is not locked
    /// </summary>
    /// <param name="set"></param>
    public void SetMouseLock(bool set)
    {
        Cursor.visible = !set;
        if (set == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }


    /// <summary>
    /// This sets the text for an NPC's response in a focused conversation
    /// </summary>
    /// <param name="speech"></param>
    public void SetFocusedSpeechSubtitle(string speech)
    {
        focusedSpeech.SetText(speech);
    }



    /// <summary>
    /// This is used for passing comments or non-focused conversations
    /// </summary>
    /// <param name="speech"></param>
    /// <param name="location"></param>
    public void SetUnfocusedSpeechSubtitle(string speech, GameObject location)
    {
        if(InactiveSpeechObjects.Count >= 1)
        {
            GameObject speechBeingActivated = InactiveSpeechObjects[0];
            speechBeingActivated.SetActive(true);
            speechBeingActivated.transform.GetChild(0).transform.GetComponent<TextMeshProUGUI>().SetText(speech);
            InactiveSpeechObjects.Remove(speechBeingActivated);
            activeSpeechObjects.Add(speechBeingActivated);
            speechBeingActivated.transform.position = location.transform.position;
        }
        else
        {
                    //Instantiate a new speech object
        }
    }


    /// <summary>
    /// This is used for a focused conversation where the user can decide how they respond
    /// </summary>
    /// <param name="conversationOptions"></param>
    public void ShowConversationOptions(List<ConversationElement> conversationOptions, bool clearOptions = false)
    {
        if (clearOptions == true) ClearPlayerSpeechOptions();
        
        if (conversationOptions != null)
        {
            isInConversation = true;
            ShowUIElement(conversationUI);
            focusedSpeech.gameObject.SetActive(true);
            foreach (ConversationElement element in conversationOptions)
            {
                if (element._isAvailable == true)
                {
                    //If there are speech object in the pool then use them
                    if (inactivePlayerSpeechOptions.Count >= 1)
                    {

                        inactivePlayerSpeechOptions[0].transform.GetComponent<ConversationButton>().SetConversationDetails(element);
                        inactivePlayerSpeechOptions[0].SetActive(true);
                        activePlayerSpeechOptions.Add(inactivePlayerSpeechOptions[0]);
                        inactivePlayerSpeechOptions.Remove(inactivePlayerSpeechOptions[0]);
                    }
                    else
                    {

                        GameObject newOption = Instantiate(speechPrefab, conversationOptionsContent.transform.position, Quaternion.identity, conversationOptionsContent.transform);
                        newOption.transform.GetComponent<ConversationButton>().SetConversationDetails(element);
                        activePlayerSpeechOptions.Add(newOption);

                    }
                }
            }
        }
       
    }

    /// <summary>
    /// This will clear all the options that appear in the dialogue interface
    /// </summary>
    protected void ClearPlayerSpeechOptions()
    {
        foreach (GameObject speechOption in activePlayerSpeechOptions)
        {
            speechOption.SetActive(false);
            inactivePlayerSpeechOptions.Add(speechOption);
        }
        activePlayerSpeechOptions.Clear();
    }

    /// <summary>
    /// This will clear all unfocused speech objects
    /// </summary>
    public virtual void ClearAllSpeechSubtitles()
    {
        foreach (GameObject speech in activeSpeechObjects)
        {
            speech.SetActive(false);
            InactiveSpeechObjects.Add(speech);
            
            
        }
        activeSpeechObjects.Clear();

    }

    //This will clear a specific unfocused speech object
    public virtual void ClearSpeechSubtitles(GameObject speechToClear)
    {

        speechToClear.SetActive(false);
        InactiveSpeechObjects.Add(speechToClear);
        activeSpeechObjects.Remove(speechToClear);


    }

    /// <summary>
    /// This is an NPC response during a focused conversation
    /// </summary>
    /// <param name="speech"></param>
    public void SetFocusedSpeech(string speech)
    {
        
            focusedSpeech.text = speech;
    }
}
