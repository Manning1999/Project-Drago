using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{

    protected bool isSelected = false;
    public bool _isSelected { get { return isSelected; } }

    [SerializeField]
    private TextMeshProUGUI textReference = null;

    [SerializeField]
    protected Color selectedColor = Color.gray;
    protected Color originalColor;

    protected Image imageComponent = null;

    protected void Start()
    {
        imageComponent = transform.GetComponent<Image>();
        originalColor = imageComponent.color;

    }

    public void SetSelected(bool set)
    {
        isSelected = set;

        if(set == true)
        {
            
            imageComponent.color = selectedColor;
        }
        else
        {
            imageComponent.color = originalColor;
        }
    }

    public void SetDetails(string title)
    {
        textReference.text = title;
    }
}
