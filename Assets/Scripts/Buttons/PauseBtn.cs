using UnityEngine;

public class PauseBtn : MonoBehaviour
{
    [SerializeField] GameObject gameCanvas;
    [SerializeField] GameObject pauseCanvas;
    [SerializeField] SoundData soundData;

    public void PauseClick()
    {
        SoundData.PlayBtn();
        gameCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
        soundData.SwitchLoopSounds(false);
        soundData.SwitchMusic(false);
        Time.timeScale = 0;
    }

}
