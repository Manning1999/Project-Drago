using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicWord : MonoBehaviour
{

    [SerializeField]
    protected string word;
    public string _word {  get { return word; } private set { word = value; } }


    [SerializeField]
    protected int baseManaUsage = 5;


    public virtual void Activate(GameObject objectOfSentence = null, MagicWord adjective = null, List<Vector3> locations = null)
    {
        
       
    }

    [ContextMenu("Learn word")]
    public void LearnWord()
    {
        MagicController.Instance.LearnMagicWord(this);
    }
    
}
