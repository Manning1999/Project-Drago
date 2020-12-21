using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UnfocusedDialogue : MonoBehaviour
{

    [SerializeField]
    private TextMeshProUGUI text = null;

    public void SetDetails(string speech, float time = 3)
    {
        text.text = speech;
        StartCoroutine(SpeechTimer(time));
    }

    private void LateUpdate()
    {
        transform.LookAt(PlayerController.Instance.transform.position);
    }


    private IEnumerator SpeechTimer(float time)
    {
        yield return new WaitForSeconds(time);
        UIController.Instance.UnsetUnfocusedDialogue(this);

    }
}
