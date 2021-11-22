using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsBtns : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] GameObject prevCanvas;
    [SerializeField] GameObject settingsCanvas;
    [SerializeField] Sprite cross;
    [SerializeField] Sprite check;
    [SerializeField] Image vibrationImage;
    [SerializeField] Image soundImage;
    [SerializeField] Image musicImage;




    public void VibrationSwitch()
    {
        settings.vibration = !settings.vibration;
        DisplayUpdate();
    }
    public void SoundSwitch()
    {
        settings.sound = !settings.sound;
        DisplayUpdate();
    }
    public void MusicSwitch()
    {
        settings.music = !settings.music;
        DisplayUpdate();
    }



    public void DisplayUpdate()
    {
        if (settings.vibration)
        {
            vibrationImage.sprite = check;
        }
        else
        {
            vibrationImage.sprite = cross;
        }

        if (settings.sound)
        {
            soundImage.sprite = check;
        }
        else
        {
            soundImage.sprite = cross;
        }

        if (settings.music)
        {
            musicImage.sprite = check;
        }
        else
        {
            musicImage.sprite = cross;
        }
    }
    public void ShowSettingsCanvas()
    {
        prevCanvas.SetActive(false);
        settingsCanvas.SetActive(true);
    }
    public void HideSettingsCanvas()
    {
        prevCanvas.SetActive(true);
        settingsCanvas.SetActive(false);
    }
}
