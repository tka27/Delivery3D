using UnityEngine;

public class MapBtn : MonoBehaviour
{
    [SerializeField] GameObject carInfoCanvas;
    [SerializeField] GameObject mapCanvas;

    public void EnableMapCanvas()
    {
        SoundData.PlayBtn();
        mapCanvas.SetActive(true);
        carInfoCanvas.SetActive(false);
    }
}
