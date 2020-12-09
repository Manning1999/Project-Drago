using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RaycastStandardInteractable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract = new UnityEvent();

    [SerializeField]
    //private bool isInteractable = true;

    bool isInteractable = true;

    bool IInteractable._isInteractable{
        get => isInteractable;
    }


    public void InteractWith()
    {
        OnInteract.Invoke();
    }



    public void SetInteractable(bool set)
    {
        isInteractable = set;
    }
}
