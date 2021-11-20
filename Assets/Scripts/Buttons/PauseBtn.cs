using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;

    public void PauseClick()
    {
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        Time.timeScale = 0;
    }

}
