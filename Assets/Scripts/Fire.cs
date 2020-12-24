using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{


    [SerializeField]
    private float damageInterval = 0.75f;

    [SerializeField]
    private int damage = 10;


    private List<IHurtable> damageables = new List<IHurtable>();


    // Start is called before the first frame update
    void OnEnable()
    {
        StartCoroutine(DamageIntervalTimer());    
    }

     

    protected IEnumerator DamageIntervalTimer()
    {
        yield return new WaitForSeconds(damageInterval);

        for(int i = damageables.Count; i > 0; i--)
        {
            damageables[i].Damage(damage);
        }
    }


    //If something that can be hurt enters the fire then add it to the list
    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.GetComponent<IHurtable>() != null)
        {
            if (!damageables.Contains(col.transform.GetComponent<IHurtable>()))
            {
                damageables.Add(col.transform.GetComponent<IHurtable>());
            }
        }   
    }

    public void OnTriggerExit(Collider col)
    {
        if (col.transform.GetComponent<IHurtable>() != null)
        {
            if (damageables.Contains(col.transform.GetComponent<IHurtable>()))
            {
                damageables.Remove(col.transform.GetComponent<IHurtable>());
            }
        }
    }
}
