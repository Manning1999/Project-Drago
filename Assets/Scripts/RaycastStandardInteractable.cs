using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class RaycastStandardInteractable : MonoBehaviour, IInteractable, IThrowable
{
    public UnityEvent OnInteract = new UnityEvent();

    [SerializeField]
    protected bool isInteractable = true;

    [SerializeField]
    private Rigidbody rb = null;
    public Rigidbody _rb { get => rb; set { rb = value; } }

    [SerializeField]
    private AnimationClip cliptToPlayBeforeThrowing = null;
    public AnimationClip _clipToPlayBeforeThrowing { get => cliptToPlayBeforeThrowing; set { cliptToPlayBeforeThrowing = value; } }

    [SerializeField]
    private Animator anim = null;
    public Animator _anim { get => anim; set { anim = value; } }

    [SerializeField]
    protected bool isThrowable = true;
    public bool _isThrowable { get => isThrowable; set { isThrowable = value; } }

    bool IInteractable._isInteractable{
        get { return isInteractable; }
    }

    UnityEvent IInteractable.OnInteract { get { return OnInteract; } }

    public void InteractWith()
    {
        OnInteract.Invoke();
    }

    public IEnumerator PlayAnimation(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {
        yield return null;
    }

    public void SetInteractable(bool set)
    {
        isInteractable = set;
    }

    public void Throw(Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {

    }
}
