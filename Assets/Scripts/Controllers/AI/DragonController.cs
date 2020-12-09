using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonController : NPC
{

    bool hasHatched = false;

    [SerializeField]
    private float dragonAge;

    [SerializeField]
    private float flyingSkill, fireBreathingSkill, magicSkill;

    [SerializeField]
    private int mana, maxMana;

    [SerializeField]
    private int flyingAge, fireBreathingAge;


    public override void InteractWith()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        if(hasHatched == false)
        {
            gameObject.SetActive(false);
        }        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Hatch()
    {
        hasHatched = true;
        gameObject.SetActive(true);
    }

    public void SetName(string nameToSet)
    {
        name = nameToSet;
    }

   
}
