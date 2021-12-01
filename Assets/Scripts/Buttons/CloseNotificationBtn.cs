using UnityEngine;

public class CloseNotificationBtn : MonoBehaviour
{
    [SerializeField] GameObject notificationCanvas;
    [SerializeField] GameObject prevCanvas;


    public void CloseNotification()
    {
        notificationCanvas.SetActive(false);
        if (prevCanvas != null)
        {
            SoundData.PlayBtn();
            prevCanvas.SetActive(true);
        }
    }
}
