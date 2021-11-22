using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] SoundData soundData;

    public void PauseClick()
    {
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        soundData.SwitchLoopSounds(false);
        Time.timeScale = 0;
    }

}
