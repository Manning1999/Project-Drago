using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brisingr : MagicVerb
{



   
    [SerializeField]
    private GameObject FirePrefab = null;

    [SerializeField]
    private static List<GameObject> activeFires = new List<GameObject>();

    [SerializeField]
    private static List<GameObject> inactiveFires = new List<GameObject>();

    private float fireSize = 1;


    public override void Activate(GameObject objectOfSentence = null, MagicAdverb adverb = null, Vector3[] locations = null)
    {


        StartCoroutine(MagicTimer(objectOfSentence, adverb, locations));


        
    }


    private IEnumerator MagicTimer(GameObject objectOfSentence = null, MagicAdverb adverb = null, Vector3[] locations = null)
    {

        if (adverb != null)
        {
            if (adverb.GetType() == typeof(Galba_Big))
            {
                fireSize = 3;
                Debug.Log("Making larger fire");
            }
            else if (adverb.GetType() == typeof(Litil_little))
            {
                fireSize = 0.3f;
            }
            else
            {
                fireSize = 1;
            }
        }

        Debug.Log("Succesfully cast");
        if (objectOfSentence != null)
        {
            if (inactiveFires.Count >= 1)
            {
                GameObject newFire = inactiveFires[0];
                activeFires.Add(newFire);
                inactiveFires.Remove(newFire);
                newFire.transform.GetComponent<Fire>().SetSize(fireSize);
                //newFire.SetActive(true);
                newFire.transform.position = objectOfSentence.transform.position;
                newFire.transform.localScale = new Vector3(fireSize, fireSize, fireSize);

            }
            else
            {
                GameObject newFire = GameObject.Instantiate(FirePrefab, objectOfSentence.transform.position, Quaternion.identity, objectOfSentence.transform);
                newFire.transform.GetComponent<Fire>().SetSize(fireSize);
            }

        }

        if (locations != null)
        {
            if (locations.Length >= 1)
            {
                for (int i = 0; i < locations.Length; i++)
                {
                    yield return new WaitForSeconds(0.03f);
                    if (inactiveFires.Count >= 1)
                    {
                        GameObject newFire = inactiveFires[0];
                        activeFires.Add(newFire);
                        inactiveFires.Remove(newFire);
                        newFire.transform.GetComponent<Fire>().SetSize(fireSize);
                        newFire.transform.position = locations[i];
                    }
                    else
                    {
                        GameObject newFire = GameObject.Instantiate(FirePrefab, locations[i], Quaternion.identity);
                        activeFires.Add(newFire);
                        newFire.transform.GetComponent<Fire>().SetSize(fireSize);
                    }

                }
            }
        }
    }


    public static void DisableFire(GameObject fire)
    {
        if (activeFires.Contains(fire))
        {
            activeFires.Remove(fire);
            inactiveFires.Add(fire);

        }
    }

}
