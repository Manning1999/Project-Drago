using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{

    //create singleton
    public static InteractionController instance;
    private static InteractionController _instance;

    public static InteractionController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<InteractionController>();
            }

            return _instance;
        }
    }


    [SerializeField]
    protected Camera interactionCamera = null;
    public Camera _interactionCamera { get { return interactionCamera; } }

    public List<GameObject> interactableObjects;

    [SerializeField]
    private GameObject objectToInteractWith = null;

    [SerializeField]
    private GameObject interactionIcon = null;

    [SerializeField]
    protected bool canInteract = true;
    public bool _canInteract { get { return canInteract; } }


    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if (canInteract == true)
        {
            if (interactableObjects.Count > 0)
            {
                GameObject currentClosest = interactableObjects[0];
                for (int i = 0; i < interactableObjects.Count; i++)
                {

                    float distance = Vector2.Distance(Camera.main.WorldToViewportPoint(interactableObjects[i].transform.GetComponent<Renderer>().bounds.center), new Vector2(0.5f, 0.5f));
                    if (distance < Vector2.Distance(Camera.main.WorldToViewportPoint(currentClosest.transform.position), new Vector2(0.5f, 0.5f)))
                    {
                        currentClosest = interactableObjects[i];
                    }
                }
                objectToInteractWith = currentClosest;
            }
            else
            {
                objectToInteractWith = null;
            }

            if (objectToInteractWith != null)
            {
                if (objectToInteractWith.transform.GetComponent<StandardInteractable>()._isInteractable == true)
                {
                    if (interactionIcon.gameObject.activeInHierarchy == false)
                    {
                        interactionIcon.gameObject.SetActive(true);

                    }

                    if (Input.GetKeyDown("e"))
                    {

                        objectToInteractWith.transform.GetComponent<IInteractable>().InteractWith();
                    }
                }
                interactionIcon.transform.position = Camera.main.WorldToScreenPoint(objectToInteractWith.transform.GetComponent<Renderer>().bounds.center);
            }
            else
            {

                if (interactionIcon.gameObject.activeInHierarchy == true) interactionIcon.gameObject.SetActive(false);

            }
        }
    }
}
