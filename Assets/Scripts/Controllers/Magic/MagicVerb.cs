using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicVerb : MagicWord
{


    [System.Serializable]
    public class CompatibleVerbs
    {
        public MagicAdverb primaryAdverb = null;
        public List<MagicAdverb> secondaryAdverbs = new List<MagicAdverb>();
    }


    [SerializeField]
    protected List<CompatibleVerbs> compatibleAdverbs = new List<CompatibleVerbs>();
    public List<CompatibleVerbs> _compatibleAdverbs { get { return compatibleAdverbs; } protected set { compatibleAdverbs = value; } }



}
