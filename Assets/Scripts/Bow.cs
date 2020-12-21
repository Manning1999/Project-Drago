using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : InventoryItem
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Use();
        }
        if (Input.GetMouseButtonUp(0))
        {
            ReleaseBow();
        }
    }

    public override void Equip(GameObject parent)
    {
        base.Equip(parent);
        transform.rotation = parent.transform.rotation;
    }

    public override void Unequip()
    {
        base.Unequip();
    }

    protected override void UseFunctionality()
    {
        DrawBow();
    }

    protected void DrawBow()
    {
        transform.GetComponent<Animator>().SetTrigger("Draw");
    }

    protected void ReleaseBow()
    {
        transform.GetComponent<Animator>().SetTrigger("Release");
    }
}
