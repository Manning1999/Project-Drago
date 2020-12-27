using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicUpgrades : MonoBehaviour
{
    protected bool isUnlocked = false;
    public bool _isUnlocked { get { return isUnlocked; } protected set { isUnlocked = value; } }

    protected Sprite icon = null;
    public Sprite _icon { get { return icon; } protected set { icon = value; } }

    public void Unlock()
    {
        isUnlocked = true;
    }
}
