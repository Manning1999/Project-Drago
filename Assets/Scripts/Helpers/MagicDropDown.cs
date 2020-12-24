using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MagicDropDown : TMP_Dropdown
{

    private List<MagicWord> words = new List<MagicWord>();

    public void AddWord(MagicWord word)
    {
        words.Add(word);

        options.Add(new TMP_Dropdown.OptionData() { text = word._word });
    }

    public void SetVerb()
    {
        MagicController.Instance.SetVerb(words[value]);
    }

    public void SetAdjective()
    {
        MagicController.Instance.SetAdjective(words[value]);
    }
}
