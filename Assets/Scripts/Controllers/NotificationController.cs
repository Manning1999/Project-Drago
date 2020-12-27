using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NotificationController : MonoBehaviour
{


    //create singleton
    public static NotificationController instance;
    private static NotificationController _instance;

    public static NotificationController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<NotificationController>();
            }

            return _instance;
        }
    }

    [SerializeField]
    private TextMeshProUGUI titleText = null;

    [SerializeField]
    private TextMeshProUGUI detailsText = null;

    [SerializeField]
    private GameObject notificationObject = null;

    private bool notificationsInProgress = false;


    private class Notification
    {
        public string title;
        public string details;

        public Notification(string newTitle, string newDetails)
        {
            title = newTitle;
            details = newDetails;
        }
    }


    private List<Notification> notificationList = new List<Notification>();


    public void CreateNotification(string title, string details)
    {
        notificationList.Add(new Notification(title, details));

        if (notificationsInProgress == false)
        {
            StartCoroutine(ShowNotification(notificationList[0]));
        }
    }





    private IEnumerator ShowNotification(Notification notification)
    {
        notificationsInProgress = true;
        titleText.SetText(notification.title);
        detailsText.SetText(notification.details);
        notificationObject.SetActive(true);
        yield return new WaitForSeconds(3);
        notificationObject.SetActive(false);
        notificationList.RemoveAt(0);

        if(notificationList.Count >= 1)
        {
            yield return new WaitForSeconds(0.3f);
            StartCoroutine(ShowNotification(notificationList[0]));
            yield return null;
        }

        notificationsInProgress = false;
    }
}
