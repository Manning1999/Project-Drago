using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildSight : MonoBehaviour
{
    [SerializeField]
    private NPC parent = null;

    NPC.NPCType enemyType;

    public void Awake()
    {
        if (parent.npcType == NPC.NPCType.Ally || parent.npcType == NPC.NPCType.ShopKeeper || parent.npcType == NPC.NPCType.Guard) enemyType = NPC.NPCType.Enemy;
        if (parent.npcType == NPC.NPCType.Enemy) enemyType = NPC.NPCType.Ally;
    }

    public void OnTriggerEnter(Collider col)
    {
        if(col.transform.GetComponent<NPC>() != null)
        {
            if (col.transform.GetComponent<NPC>().npcType == enemyType)
            {
                parent.AddEnemy(col.transform.GetComponent<NPC>());
            }
        }    
    }


    public void OnTriggerExit(Collider col)
    {
        if (col.transform.GetComponent<NPC>() != null)
        {
            if (col.transform.GetComponent<NPC>().npcType == enemyType)
            {
                parent.RemoveEnemy(col.transform.GetComponent<NPC>());
            }
        }
    }
}
