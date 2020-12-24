using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class MagicController : MonoBehaviour
{


    //create singleton
    public static MagicController instance;
    private static MagicController _instance;

    public static MagicController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<MagicController>();
            }

            return _instance;
        }
    }



    [SerializeField]
    private int playerMaxMana = 100;

    [SerializeField]
    private int playerCurrentMana = 100;

    [SerializeField]
    protected GameObject sentenceObject = null;

    [SerializeField]
    private MagicWord sentenceVerb = null;

    [SerializeField]
    private MagicWord sentenceAdjective = null;

    private List<Vector3> spellLocations = new List<Vector3>();


    protected List<MagicWord> knownMagicWords = new List<MagicWord>();

    List<TMP_Dropdown.OptionData> dropDownOptions = new List<TMP_Dropdown.OptionData>();

    [SerializeField]
    private TrailRenderer magicTrail = null;

    [SerializeField]
    private MagicDropDown verbDropDown, adjectiveDropDown;

    bool isDoingMagic = false;

    [SerializeField]
    Camera camera = null;

    private void Start()
    {

    }

    private void Update()
    {

        if(isDoingMagic == true)
        {
            if (Input.GetMouseButtonDown(1))
            {
                spellLocations.Clear();
                magicTrail.Clear();
                
            }

            if (Input.GetMouseButton(1))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);

                      Vector3 dir = (hit.point - camera.transform.position).normalized;
                      AddMagicLocation(camera.transform.position + dir * (hit.distance - 0.05f));
                    // AddMagicLocation(hit.point);
                }
            }

            
        }
    }


  

    public void Speak()
    {
        Debug.Log("Doing magic");
        if(sentenceVerb != null)
        {

            if(sentenceObject != null)
            {
                if(sentenceAdjective != null)
                {
                    if (spellLocations.Count >= 1)
                    {
                        sentenceVerb.Activate(sentenceObject, sentenceAdjective, spellLocations);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject, sentenceAdjective);
                    }
                }
                else
                {

                }
            }
            else
            {
                if (sentenceAdjective != null)
                {
                    if (spellLocations.Count >= 1)
                    {
                        sentenceVerb.Activate(sentenceObject, sentenceAdjective, spellLocations);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject);
                    }
                }
                else
                {
                    if (spellLocations.Count >= 1)
                    {
                        Debug.Log("Casting brisingr");
                        sentenceVerb.Activate(sentenceObject, null, spellLocations);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject);
                    }
                }
            }
        }
    }

    public void SetIsDoingMagic(bool set)
    {
        isDoingMagic = set;
        magicTrail.Clear();
    }


    public void SetObject(GameObject objectToSet)
    {

    }

    public void SetVerb(MagicWord word)
    {
        sentenceVerb = word;
    }



    public void SetAdjective(MagicWord word)
    {
        sentenceAdjective = word;
    }




    public void AddMagicLocation(Vector3 point)
    {
        spellLocations.Add(point);
        magicTrail.transform.position = point;
    }



    public void ResetMagicLocations()
    {
        spellLocations.Clear();
    }



    public void SetMagicWordDropdowns()
    {
        Debug.Log(knownMagicWords.Count);
        for (int i = dropDownOptions.Count; i > 0; i--)
        {

            //If the word is a verb then add it to the list
            if (knownMagicWords[i - 1].GetType().IsSubclassOf(typeof(MagicVerb)))
            {
                if (!verbDropDown.options.Contains(dropDownOptions[i-1]))
                {
                    verbDropDown.options.Add(dropDownOptions[i-1]);
                }
            }

            //If the word is an adjective then add it to the list
            if (knownMagicWords[i-1].GetType().IsSubclassOf(typeof(MagicAdjective)))
            {
                if (!adjectiveDropDown.options.Contains(dropDownOptions[i-1]))
                {
                    adjectiveDropDown.options.Add(dropDownOptions[i-1]);
                }
            }
        }
    }

    //Add a magic word to the list and then create a new dropdown option for it
    public void LearnMagicWord(MagicWord newMagicWord)
    {
        knownMagicWords.Add(newMagicWord);

        //If the word is a verb then add it to the list
        if (newMagicWord.GetType().IsSubclassOf(typeof(MagicVerb)))
        {

            verbDropDown.AddWord(newMagicWord);

        }

        //If the word is an adjective then add it to the list
        if (newMagicWord.GetType().IsSubclassOf(typeof(MagicAdjective)))
        {

            adjectiveDropDown.AddWord(newMagicWord);

        }
    }


}
