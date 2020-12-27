using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicVerb : MagicWord
{

    [SerializeField]
    protected List<MagicAdverb> compatibleAdverbs = new List<MagicAdverb>();
    public List<MagicAdverb> _compatibleAdverbs { get { return compatibleAdverbs; } protected set { compatibleAdverbs = value; } }

}
