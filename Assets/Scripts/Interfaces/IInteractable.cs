using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable 
{
        bool _isInteractable { get; }
        void InteractWith();
}
