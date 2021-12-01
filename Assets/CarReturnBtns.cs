using UnityEngine;

public class CarReturnBtns : MonoBehaviour
{
    [SerializeField] GameObject carReturnCanvas;
    [SerializeField] GameObject prevCanvas;

    public delegate void ReturnHandler();
    public static event ReturnHandler returnEvent;

    public void ActivateCarReturnCanvas()
    {
        carReturnCanvas.SetActive(true);
        prevCanvas.SetActive(false);
    }

    public void ReturnConfirm()
    {
        if (SceneData.lastVisit) returnEvent.Invoke();
        
        carReturnCanvas.SetActive(false);
        prevCanvas.SetActive(true);
    }
}
