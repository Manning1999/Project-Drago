using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicStorage : InventoryItem
{

    protected int manaStored = 0;
    public int _manaStored { get { return manaStored; } protected set { manaStored = value; } }


    /// <summary>
    /// This will reduce the objects mana pool
    /// </summary>
    /// <param name="manaToUse"></param>
    public void UseMana(int manaToUse)
    {
        manaStored -= manaToUse;
    }


    /// <summary>
    /// This will increase the objects mana pool
    /// </summary>
    /// <param name="manaToAdd"></param>
    public void AddMana(int manaToAdd)
    {
        manaStored += manaToAdd;
    }
}
