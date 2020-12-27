using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

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
    private Color selectedObjectOutlineColor, unselectedObjectOutlineColor;

    [SerializeField]
    private int playerMaxMana = 100;

    [SerializeField]
    private int playerCurrentMana = 100;

    [SerializeField]
    protected GameObject sentenceObject = null;

    [SerializeField]
    private MagicVerb sentenceVerb = null;

    [SerializeField]
    private MagicAdverb sentenceAdverb = null;

    private List<Vector3> spellLocations = new List<Vector3>();


    protected List<MagicWord> knownMagicWords = new List<MagicWord>();

    List<TMP_Dropdown.OptionData> dropDownOptions = new List<TMP_Dropdown.OptionData>();

    [SerializeField]
    private TrailRenderer magicTrail = null;

    [SerializeField]
    private MagicDropDown verbDropDown, adverbDropDown;

    [SerializeField]
    private Button castButton = null; 

    bool isDoingMagic = false;

    [SerializeField]
    Camera camera = null;

    private bool iSAllowedToDraw = false;

    [SerializeField]
    private GameObject learntMagicEfect = null;

    ColorGrading mainCameraColorGradingLayer = null;

    [SerializeField]
    Camera magicalCamera = null;

    [SerializeField]
    PostProcessVolume ppVol = null;

    int originalCullingMask;


    private void Start()
    {

        ppVol.profile.TryGetSettings(out mainCameraColorGradingLayer);
        originalCullingMask = camera.cullingMask;
    }

    private void Update()
    {

        if (isDoingMagic == true)
        {
            


            if (sentenceVerb != null)
            {
                if (sentenceVerb._canDraw == true)
                {
                    if (sentenceAdverb != null)
                    {
                        if (sentenceAdverb._canDraw == true)
                        {
                            iSAllowedToDraw = true;
                        }
                        else
                        {
                            iSAllowedToDraw = false;
                        }
                    }
                    else
                    {
                        iSAllowedToDraw = true;
                    }
                }
                else
                {
                    if (sentenceAdverb != null)
                    {
                        if (sentenceAdverb._canDraw == true)
                        {
                            iSAllowedToDraw = true;
                        }
                        else
                        {
                            iSAllowedToDraw = false;
                        }
                    }
                }
            }

            if (iSAllowedToDraw == true)
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    Vector3 dir = (hit.point - camera.transform.position).normalized;

                    if (Input.GetMouseButtonDown(1))
                    {
                        spellLocations.Clear();
                        AddMagicLocation(camera.transform.position + dir * (hit.distance - 0.01f));
                        magicTrail.Clear();


                    }

                    if (Input.GetMouseButton(1) && !Input.GetMouseButtonUp(1))
                    {

                        Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.yellow);


                        AddMagicLocation(camera.transform.position + dir * (hit.distance - 0.01f));

                    }

                    if (Input.GetMouseButtonUp(1))
                    {
                        if(sentenceVerb._doesDrawStartToEnd == true)
                        {
                            if (sentenceVerb._requiresStartOnMagicObject == true)
                            {
                                magicTrail.Clear();
                                Vector3 firstPos = sentenceObject.transform.position;
                                Vector3 lastPos = spellLocations[spellLocations.Count - 1];
                                //Debug.Break();
                                ResetMagicLocations();
                                spellLocations.Add(firstPos);
                                spellLocations.Add(lastPos);
                                magicTrail.AddPosition(firstPos);
                                magicTrail.AddPosition(lastPos);
                                
                            }
                            else
                            {


                                magicTrail.Clear();
                                Vector3 firstPos = spellLocations[0];
                                Vector3 lastPos = spellLocations[spellLocations.Count - 1];
                                //Debug.Break();
                                ResetMagicLocations();
                                magicTrail.AddPosition(firstPos);
                                magicTrail.AddPosition(lastPos);
                                
                            }

                        }
                    }
                }
            }


            if (Input.GetMouseButtonDown(0))
            {

                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out hit))
                {
                    
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Magical"))
                    {
                        SetObject(hit.transform.gameObject);
                    }

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
                if(sentenceAdverb != null)
                {
                    if (spellLocations.Count >= 1)
                    {
                        Vector3[] positions = new Vector3[magicTrail.positionCount];
                        magicTrail.GetPositions(positions);
                        sentenceVerb.Activate(sentenceObject, sentenceAdverb, positions);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject, sentenceAdverb);
                    }
                }
                else
                {
                    if (spellLocations.Count >= 1)
                    {
                        Vector3[] positions = new Vector3[magicTrail.positionCount];
                        magicTrail.GetPositions(positions);
                        sentenceVerb.Activate(sentenceObject, null, positions);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject);
                    }
                }
            }
            else
            {
                if (sentenceAdverb != null)
                {
                    if (spellLocations.Count >= 1)
                    {
                        Vector3[] positions = new Vector3[magicTrail.positionCount];
                        magicTrail.GetPositions(positions);
                        sentenceVerb.Activate(sentenceObject, sentenceAdverb, positions);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject, sentenceAdverb);
                    }
                }
                else
                {
                    if (spellLocations.Count >= 1)
                    {
                        Debug.Log("Casting brisingr");
                        Vector3[] positions = new Vector3[magicTrail.positionCount];
                        magicTrail.GetPositions(positions);
                        sentenceVerb.Activate(sentenceObject, null, positions);
                    }
                    else
                    {
                        sentenceVerb.Activate(sentenceObject);
                    }
                }
            }
        }

        UIController.Instance.ShowUIElement(null);
        SetIsDoingMagic(false);
    }

    public void SetIsDoingMagic(bool set)
    {
        isDoingMagic = set;
        magicTrail.Clear();

        if (set == true)
        {
            mainCameraColorGradingLayer.saturation.value = -100f;
            camera.cullingMask &= ~(1 << LayerMask.NameToLayer("Magical"));
        }
        else
        {
            mainCameraColorGradingLayer.saturation.value = 50f;
            SetObject(null);
            camera.cullingMask = originalCullingMask;
        }
        magicalCamera.gameObject.SetActive(set);

    }


    public void SetObject(GameObject objectToSet)
    {
        if(sentenceObject != null)
        {
            sentenceObject.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", unselectedObjectOutlineColor);
        }
        if (objectToSet != null)
        {
            sentenceObject = objectToSet;
            objectToSet.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", selectedObjectOutlineColor);
        }
    }

    public void SetVerb(MagicVerb word)
    {
        sentenceVerb = word;

        if(word._compatibleAdverbs.Count >= 1)
        {
            SetMagicWordDropdowns();
            adverbDropDown.gameObject.SetActive(true);
        }
        else
        {
            adverbDropDown.gameObject.SetActive(false);
        }

        

    }



    public void SetAdverb(MagicAdverb word)
    {

        sentenceAdverb = word;
       
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


    /// <summary>
    /// This will add words to the dropdown menus 
    /// </summary>
    public void SetMagicWordDropdowns()
    {
        adverbDropDown.ClearOptions();


        adverbDropDown.AddWord(null);
        for (int i = knownMagicWords.Count; i > 0; i--)
        {

            //If the word is an adjective then add it to the list
            if (knownMagicWords[i-1].GetType().IsSubclassOf(typeof(MagicAdverb)))
            {
                if (!adverbDropDown.options.Contains(dropDownOptions[i-1]) && sentenceVerb._compatibleAdverbs.Contains((MagicAdverb)knownMagicWords[i-1]))
                {
                    adverbDropDown.AddWord(knownMagicWords[i - 1]);
                }
            }
        }
    }

    /// <summary>
    /// Add a magic word to the list and then create a new dropdown option for it. This will also start the "learnt magic" effect timer
    /// </summary>
    /// <param name="newMagicWord"></param>
    public void LearnMagicWord(MagicWord newMagicWord)
    {
        if (!knownMagicWords.Contains(newMagicWord))
        {
            knownMagicWords.Add(newMagicWord);
            learntMagicEfect.transform.position = PlayerController.Instance.transform.position + PlayerController.Instance.transform.forward * 1;
            learntMagicEfect.transform.LookAt(PlayerController.Instance.transform.position);
            learntMagicEfect.SetActive(true);
            dropDownOptions.Add(new TMP_Dropdown.OptionData() { text = newMagicWord._word });

            StartCoroutine(LearntMagicEffectTimer());


            //If the word is a verb then add it to the list
            if (newMagicWord.GetType().IsSubclassOf(typeof(MagicVerb)))
            {

                verbDropDown.AddWord(newMagicWord);

            }
        }

    }

    /// <summary>
    /// This will make the learnt magic effect dissapear after a set amount of time
    /// </summary>
    /// <returns></returns>
    private IEnumerator LearntMagicEffectTimer()
    {
        yield return new WaitForSeconds(3);
        learntMagicEfect.SetActive(false);
    }


}
