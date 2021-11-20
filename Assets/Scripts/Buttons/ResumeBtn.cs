using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeBtn : MonoBehaviour
{
    
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;

    public void PlayClick()
    {
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        Time.timeScale = 1;
    }
}
