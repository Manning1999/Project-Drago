using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private ParticleSystem ps = null;


    [SerializeField]
    private float damageInterval = 0.75f;

    [SerializeField]
    private int damage = 10;

    [SerializeField]
    private float lifeTime = 7f;

    float fireSize = 1;

    int baseMaxParticles;
    float baseRateOverTime;
    float baseRadius;
    float baseSize;
    float baseLifeTime;

    ParticleSystem partSys;
    ParticleSystem.MainModule main;
    ParticleSystem.EmissionModule emission;
    ParticleSystem.ShapeModule shape;
    ParticleSystem.SizeOverLifetimeModule size;

    


    private List<IHurtable> damageables = new List<IHurtable>();


    // Start is called before the first frame update
    void OnEnable()
    {
        if (partSys == null)
        {
            partSys = transform.GetComponent<ParticleSystem>();
            main = partSys.main;
            emission = partSys.emission;
            shape = partSys.shape;
            size = partSys.sizeOverLifetime;

            baseMaxParticles = main.maxParticles;
            baseRateOverTime = emission.rateOverTime.constant;
            baseRadius = shape.radius;
            baseLifeTime = main.startLifetime.constant;
        }


        StartCoroutine(DamageIntervalTimer());    
        StartCoroutine(LifetimeTimer());  

    }

    public void SetSize(float newSize)
    {
        fireSize = newSize;
        emission.rateOverTime = baseRateOverTime / (fireSize / 2);
       // main.maxParticles = (int)(baseMaxParticles / fireSize * 2);
        shape.radius = baseRadius * fireSize * 2;
        size.sizeMultiplier = fireSize;
        main.startLifetime = baseLifeTime * fireSize;
        gameObject.SetActive(true);
        main.loop = true;
        partSys.Clear();
        StartCoroutine(DamageIntervalTimer());
        StartCoroutine(LifetimeTimer());
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

    private IEnumerator LifetimeTimer()
    {
        yield return new WaitForSeconds(lifeTime);
        
        main.loop = false;
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        Brisingr.DisableFire(gameObject);
    }
}
