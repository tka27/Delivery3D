using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseNotificationBtn : MonoBehaviour
{
    [SerializeField] GameObject notificationCanvas;
    [SerializeField] GameObject prevCanvas;


    public void CloseNotification()
    {
        notificationCanvas.SetActive(false);
        prevCanvas?.SetActive(true);
    }
}
