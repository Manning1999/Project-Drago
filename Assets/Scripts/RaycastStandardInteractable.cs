using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RaycastStandardInteractable : MonoBehaviour, IInteractable
{
    public UnityEvent OnInteract = new UnityEvent();

    [SerializeField]
    protected bool isInteractable = true;

    bool IInteractable._isInteractable{
        get { return isInteractable; }
    }

    UnityEvent IInteractable.OnInteract { get { return OnInteract; } }

    public void InteractWith()
    {
        OnInteract.Invoke();
    }



    public void SetInteractable(bool set)
    {
        isInteractable = set;
    }
}
