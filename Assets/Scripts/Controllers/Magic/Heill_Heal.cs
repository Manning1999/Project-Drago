using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heill_Heal : MagicVerb
{


    private float healInterval = 1;
    private int healAmount = 5;
    bool stop = false;

    public override void Activate(GameObject objectOfSentence = null, MagicAdverb adjective = null, Vector3[] locations = null)
    {

        if(adjective != null)
        {
            
        }
        else
        {

        }

        if(objectOfSentence != null)
        {
            if (objectOfSentence.transform.GetComponent<IHurtable>() != null)
            {

                StartCoroutine(HealTimer(objectOfSentence));
            }
        }
    }

    protected IEnumerator HealTimer(GameObject objectOfSentence = null)
    {
        
        yield return new WaitForSeconds(healInterval);
        objectOfSentence.transform.GetComponent<IHurtable>().Heal(healAmount);
        if(stop == false)
        {
            StartCoroutine(HealTimer(objectOfSentence));
        }
    }

}
