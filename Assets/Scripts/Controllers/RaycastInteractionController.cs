using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastInteractionController : MonoBehaviour
{


    //create singleton
    public static RaycastInteractionController instance;
    private static RaycastInteractionController _instance;

    public static RaycastInteractionController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<RaycastInteractionController>();
            }

            return _instance;
        }
    }



    [SerializeField]
    private float interactionDistance = 2;

    [SerializeField]
    private GameObject interactionIcon = null;

    [SerializeField]
    protected LayerMask raycastLayers;

    protected GameObject objectToInteractWith = null;

    [SerializeField]
    protected bool canInteract = true;
    public bool _canInteract { get { return canInteract; } }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (canInteract == true)
        {
            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, raycastLayers))
            {
                if (hit.transform.gameObject != objectToInteractWith)
                {
                    if (hit.transform.GetComponent<IInteractable>() != null)
                    {
                        if (hit.transform.GetComponent<IInteractable>()._isInteractable == true)
                        {
                            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                            objectToInteractWith = hit.transform.gameObject;

                            interactionIcon.SetActive(true);
                        }
                        else
                        {
                            objectToInteractWith = null;
                            Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.white);
                            interactionIcon.SetActive(false);
                        }
                    }
                    else
                    {
                        objectToInteractWith = null;
                        Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.white);
                        interactionIcon.SetActive(false);
                    }
                }
            }
            else
            {
                objectToInteractWith = null;
                Debug.DrawRay(transform.position, transform.forward * interactionDistance, Color.white);
                interactionIcon.SetActive(false);
            }


            if (objectToInteractWith != null)
            {
                if (Input.GetKeyDown("e"))
                {
                    hit.transform.GetComponent<IInteractable>().InteractWith();
                }
            }
        }
    }



    /// <summary>
    /// This sets the CanInteract variable. This variable decides whether or not the player will be able to interact with anything
    /// E.g. Set to false when in a conversation
    /// </summary>
    /// <param name="set"></param>
    public void SetCanInteract(bool set)
    {
        canInteract = set;
    }
}
