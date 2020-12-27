using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MagicDropDown : TMP_Dropdown
{

    public Toggle[] toggles;

    private List<MagicWord> words = new List<MagicWord>();

    public void AddWord(MagicWord word)
    {

        words.Add(word);
        // toggles.Add()

        if (word != null)
        {
            options.Add(new TMP_Dropdown.OptionData() { text = word._word });
        }
        else
        {
            options.Add(new TMP_Dropdown.OptionData() { text = "None" });
        }
    }

    public void SetVerb()
    {
        MagicController.Instance.SetVerb((MagicVerb)words[value]);
    }

    public void SetAdverb()
    {
        MagicController.Instance.SetAdverb((MagicAdverb)words[value]);
    }



}

