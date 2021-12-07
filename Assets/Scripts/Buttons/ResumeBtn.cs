using UnityEngine;

public class ResumeBtn : MonoBehaviour
{
    
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] SoundData soundData;
    [SerializeField] GameSettings settings;

    public void PlayClick()
    {
        SoundData.PlayBtn();
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        soundData.SwitchLoopSounds(settings.sound);
        soundData.SwitchMusic(settings.music);
        Time.timeScale = 1;
    }
}
