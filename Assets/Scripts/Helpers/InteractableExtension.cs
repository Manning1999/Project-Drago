using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InteractableExtension
{
   public static void InteractWith(this IInteractable iinteractable)
    {
        if(iinteractable._isInteractable == true)
        {
            iinteractable.OnInteract.Invoke();

            iinteractable.InteractWith();
        }
    }
}
