using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ThrowableExtension
{

   
    /// <summary>
    /// This will decide whether to play the animation first before throwing the object
    /// </summary>
    /// <param name="ithrowable"></param>
    /// <param name="mono"></param>
    /// <param name="startPosition"></param>
    /// <param name="locationToThrowTo"></param>
    /// <param name="throwForce"></param>
    public static void Activate(this IThrowable iThrowable, MonoBehaviour mono, Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {
        iThrowable._rb.velocity = Vector3.zero;
        iThrowable._rb.angularVelocity = Vector3.zero;
        mono.gameObject.SetActive(true);


        if (iThrowable._clipToPlayBeforeThrowing != null)
        {
            mono.StartCoroutine(iThrowable.StartAnimation(startPosition, locationToThrowTo, throwForce));
        }
        else
        {
            iThrowable.ThrowItem(startPosition, locationToThrowTo, throwForce);

        }
    }


    /// <summary>
    /// Plays a quick "buildup" animation
    /// </summary>
    /// <param name="ithrowable"></param>
    /// <param name="mono"></param>
    /// <param name="startPosition"></param>
    /// <param name="locationToThrowTo"></param>
    /// <param name="throwForce"></param>
    /// <returns></returns>
    public static IEnumerator StartAnimation(this IThrowable iThrowable, Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {
        iThrowable.PlayAnimation(startPosition, locationToThrowTo, throwForce);
        iThrowable._anim.Play(iThrowable._clipToPlayBeforeThrowing.name);
        yield return new WaitForSeconds(iThrowable._clipToPlayBeforeThrowing.length);
        iThrowable.ThrowItem(startPosition, locationToThrowTo, throwForce);
        

        
    }
    public static void ThrowItem(this IThrowable iThrowable, Vector3 startPosition, Vector3 locationToThrowTo, float throwForce)
    {

        Vector3 dir = (startPosition - locationToThrowTo).normalized;
        iThrowable._rb.AddForce(-dir * throwForce, ForceMode.Impulse);
        Debug.Log("Thrown");
        iThrowable.Throw(startPosition, locationToThrowTo, throwForce);

    }

   


}
