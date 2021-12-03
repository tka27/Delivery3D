using UnityEngine;

public class CloseNotificationBtn : MonoBehaviour
{
    [SerializeField] GameObject notificationCanvas;
    [SerializeField] GameObject prevCanvas;


    public void CloseNotification()
    {
        notificationCanvas.SetActive(false);
        SoundData.PlayBtn();
        if (prevCanvas != null)
        {
            prevCanvas.SetActive(true);
        }
    }
}
