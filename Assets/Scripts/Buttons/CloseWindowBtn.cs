using UnityEngine;

public class CloseWindowBtn : MonoBehaviour
{
    [SerializeField] GameObject currentCanvas;
    [SerializeField] GameObject prevCanvas;


    public void CloseWindow()
    {
        currentCanvas.SetActive(false);
        SoundData.PlayBtn();
        if (prevCanvas != null)
        {
            prevCanvas.SetActive(true);
        }
    }
}
