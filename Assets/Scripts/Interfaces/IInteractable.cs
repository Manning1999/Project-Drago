using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable 
{
        UnityEvent OnInteract { get; }
        bool _isInteractable { get; }
        void InteractWith();
}
