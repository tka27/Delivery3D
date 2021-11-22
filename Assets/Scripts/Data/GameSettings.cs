using UnityEngine;

[CreateAssetMenu]
public class GameSettings : ScriptableObject
{
    [HideInInspector] public bool vibration;
    [HideInInspector] public bool sound;
    [HideInInspector] public bool music;

    public void LoadPrefs() 
    {
        vibration = PlayerPrefs.GetInt("vibration") == 1;
        sound = PlayerPrefs.GetInt("sound") == 1;
        music = PlayerPrefs.GetInt("music") == 1;
    }

    public void SavePrefs()
    {
        if (vibration)
        {
            PlayerPrefs.SetInt("vibration", 1);
        }
        else
        {
            PlayerPrefs.SetInt("vibration", 0);
        }

        if (sound)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
        }

        if (music)
        {
            PlayerPrefs.SetInt("music", 1);
        }
        else
        {
            PlayerPrefs.SetInt("music", 0);
        }
        PlayerPrefs.Save();
    }
}
