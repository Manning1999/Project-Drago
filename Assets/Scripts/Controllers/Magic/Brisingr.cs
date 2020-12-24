using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brisingr : MagicVerb
{


   
    [SerializeField]
    private GameObject FirePrefab = null;


    public override void Activate(GameObject objectOfSentence = null, MagicWord adjective = null, List<Vector3> locations = null)
    {
        Debug.Log("Succesfully cast");
        if(objectOfSentence != null)
        {
            //Instantiate fire at the gameobject's location
            GameObject newFire = GameObject.Instantiate(FirePrefab, objectOfSentence.transform.position, Quaternion.identity, objectOfSentence.transform);

        }

        if(locations.Count >= 1)
        {
            for(int i = locations.Count; i > 0; i--)
            {
                GameObject.Instantiate(FirePrefab, locations[i - 1], Quaternion.identity);
            }
        }

        
    }

}
