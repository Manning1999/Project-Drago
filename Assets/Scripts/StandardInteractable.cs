using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StandardInteractable : MonoBehaviour, IInteractable
{

    public UnityEvent OnInteract = new UnityEvent();

    protected bool isInteractable = true;

    public bool _isInteractable
    {
        get => isInteractable;
    }

    [SerializeField]
    protected AudioClip interactionSound;

    [SerializeField]
    protected AudioSource audioSource;


    Plane[] planes;

    Bounds bounds;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isInteractable = true;
        audioSource = transform.GetComponent<AudioSource>();
        planes = GeometryUtility.CalculateFrustumPlanes(InteractionController.Instance._interactionCamera);

        try
        {
            bounds = GetComponent<Collider>().bounds;
        }
        catch(Exception e)
        {
             bounds = GetComponent<MeshRenderer>().bounds;
        }  
    }

    // Update is called once per frame
    protected virtual void Update()
    {

        if (transform.GetComponent<Renderer>().IsVisibleFrom(InteractionController.Instance._interactionCamera))
        {
            Debug.Log("Is visible");
            if (!InteractionController.Instance.interactableObjects.Contains(this.gameObject))
            {
                InteractionController.Instance.interactableObjects.Add(this.gameObject);
            }
        }
        else
        {
            if (InteractionController.Instance.interactableObjects.Contains(this.gameObject))
            {
                InteractionController.Instance.interactableObjects.Remove(this.gameObject);
                Debug.Log("Removing now");

            }
        }   



        if (GeometryUtility.TestPlanesAABB(planes, bounds))
        {
            Debug.Log("Is visible");
            if (!InteractionController.Instance.interactableObjects.Contains(this.gameObject))
            {
                InteractionController.Instance.interactableObjects.Add(this.gameObject);
            }
        }
        else
        {
            if (InteractionController.Instance.interactableObjects.Contains(this.gameObject))
            {
                InteractionController.Instance.interactableObjects.Remove(this.gameObject);
                Debug.Log("Removing now");

            }
        }


    }

    public virtual void setInteractable(bool set)
    {
        Debug.Log("Set");
        isInteractable = set;
    }

    public void InteractWith()
    {
        OnInteract.Invoke();
    }

    public void OnDisable()
    {
        if (InteractionController.instance != null)
        {
            if (InteractionController.Instance.interactableObjects.Contains(this.gameObject))
            {
                InteractionController.Instance.interactableObjects.Remove(this.gameObject);

            }
        }
        else
        {
            Debug.LogError("Scene Requires an InteractionController");
        }
    }


}
