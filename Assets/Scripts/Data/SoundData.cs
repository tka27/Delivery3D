using UnityEngine;
using System.Collections.Generic;

public class SoundData : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] AudioSource coin;
    public List<AudioSource> loopSounds;

    public void PlayCoin()
    {
        if (!settings.sound) return;
        coin.pitch = Random.Range(0.8f, 1.5f);
        coin.Play();
    }

    public void SwitchLoopSounds(bool status)
    {
        foreach (var sound in loopSounds)
        {
            if (status && !sound.isPlaying)
            {
                sound.Play();
            }
            else if (!status)
            {
                sound.Stop();
            }
        }
    }
}
