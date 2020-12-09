using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : HurtableObject, IInteractable
{

    [SerializeField]
    protected string name = "";

    [SerializeField]
    [Tooltip("If true, an NPC will be considered essential and therefore cannot be killed, they will simply be rendered unconcious instead of being killed")]
    bool isEssential = true;

    public enum NPCType { Enemy = 0, Ally = 1, ShopKeeper = 2, Guard = 3, Wanderer = 4}

    [SerializeField]
    public NPCType npcType = NPCType.Ally;

    protected bool isInteractable = true;
    public bool _isInteractable
    {
        get => isInteractable;
    }

    public virtual void InteractWith()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Die()
    {
        if(isEssential == true)
        {
            //Render unconcious
        }
        else
        {
            base.Die();
        }
    }
}
