using UnityEngine;
using System.Collections.Generic;
using System;

public class SoundData : MonoBehaviour
{
    [SerializeField] GameSettings settings;
    [SerializeField] AudioSource coinSource;
    [SerializeField] List<AudioClip> coinsClips;
    [SerializeField] AudioSource btnSource;
    public List<AudioSource> loopSounds;

    static event Action playCoinEvent;
    static event Action playBtnEvent;



    void Start()
    {
        playCoinEvent += CoinSound;
        playBtnEvent += BtnSound;
    }

    void CoinSound()
    {
        if (!settings.sound) return;
        int randomIndex = UnityEngine.Random.Range(0, coinsClips.Count);
        coinSource.clip = coinsClips[randomIndex];
        coinSource.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        coinSource.Play();
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

    void BtnSound()
    {
        if (!settings.sound) return;
        btnSource.pitch = UnityEngine.Random.Range(0.95f, 1.05f);
        btnSource.Play();
    }
    public static void PlayCoin()
    {
        playCoinEvent.Invoke();
    }

    public static void PlayBtn()
    {
        playBtnEvent.Invoke();
    }
}
