using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrautha_Throw : MagicVerb
{

    float throwForce = 30;

    public override void Activate(GameObject objectOfSentence = null, MagicAdverb adverb = null, Vector3[] locations = null)
    {
        if(objectOfSentence != null)
        {
            if(adverb != null)
            {
                
            }
            else
            {
                if (locations != null)
                {
                    Vector3 dir = (locations[0] - locations[locations.Length - 1]).normalized;
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(-dir * throwForce, ForceMode.Impulse);
                }
                else
                {
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(PlayerController.Instance.transform.forward * throwForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            if (adverb != null)
            {

            }
            else
            {

            }
        }
    }
}
