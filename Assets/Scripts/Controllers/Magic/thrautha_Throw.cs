using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrautha_Throw : MagicVerb
{

    float throwForce = 40;

    [SerializeField]
    private GameObject rockPrefab = null;

    private static List<GameObject> activeRocks = new List<GameObject>();
    private static List<GameObject> inactiveRocks = new List<GameObject>();

    private Vector3 directionOverride = Vector3.zero;




    public override void Activate(GameObject objectOfSentence = null, MagicAdverb adverb = null, Vector3[] locations = null)
    {

        if (adverb != null)
        {
            if (adverb.GetType() == typeof(Galba_Big))
            {
                throwForce = 80;

            }
            else if (adverb.GetType() == typeof(Litil_little))
            {
                throwForce = 10;
            }
            else
            {
                throwForce = 40;
            }

            if(adverb.GetType() == typeof(Edtha_Me))
            {
                directionOverride = (objectOfSentence.transform.position - PlayerController.Instance.transform.position).normalized;
            }
        }

        if (objectOfSentence != null)
        {


            if (locations != null)
            {
                if (directionOverride == Vector3.zero)
                {
                    Vector3 dir = (locations[0] - locations[locations.Length - 1]).normalized;
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(-dir * throwForce, ForceMode.Impulse);
                }
                else
                {
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(-directionOverride * throwForce, ForceMode.Impulse);
                }
            }
            else
            {
                if (directionOverride == Vector3.zero)
                {
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(PlayerController.Instance.transform.forward * throwForce, ForceMode.Impulse);
                }
                else
                {
                    objectOfSentence.transform.GetComponent<Rigidbody>().AddForce(-directionOverride * throwForce, ForceMode.Impulse);
                }
            }
        }
        else
        {

            if (inactiveRocks.Count >= 1)
            {
                GameObject newRock = inactiveRocks[0];
                newRock.transform.position = locations[0];
                activeRocks.Add(newRock);
                inactiveRocks.Remove(newRock);
                newRock.transform.GetComponent<IThrowable>().Activate(newRock.transform.GetComponent<MonoBehaviour>(), newRock.transform.position, locations[1], throwForce);
            }
            else
            {
                GameObject newRock = Instantiate(rockPrefab, locations[0], Quaternion.identity);
                activeRocks.Add(newRock);
                newRock.transform.GetComponent<IThrowable>().Activate(newRock.transform.GetComponent<MonoBehaviour>(), newRock.transform.position, locations[1], throwForce);

                Debug.Log("Instantiating now");
            }

        }
    }


    public static void DeactivateRock(GameObject rockToDeactivate)
    {
        inactiveRocks.Add(rockToDeactivate);
        activeRocks.Remove(rockToDeactivate);
    }
}
