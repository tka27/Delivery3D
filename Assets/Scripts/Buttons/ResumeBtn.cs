using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeBtn : MonoBehaviour
{
    
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] SoundData soundData;
    [SerializeField] GameSettings settings;

    public void PlayClick()
    {
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        soundData.SwitchLoopSounds(settings.sound);
        Time.timeScale = 1;
    }
}
