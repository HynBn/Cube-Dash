using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioSource menuMusicSource;
    [SerializeField] private AudioSource inGameMusicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Audio Clip")]
    public AudioClip menuMusic;
    public AudioClip inGameMusic;
    public AudioClip buttonPressSFX;
    public AudioClip coinSFX;
    public AudioClip gameOverSFX;

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.clip = clip;
        sfxSource.Play();
    }

    public void PlayMenuMusic()
    {
        menuMusicSource.clip = menuMusic;
        menuMusicSource.Play();
    }

    public void PlayinGameMusic(bool fromStart)
    {
        inGameMusicSource.clip = inGameMusic;

        if (fromStart)
        {
            inGameMusicSource.time = 0;
        }

        inGameMusicSource.Play();
    }

    public void StopMusic(AudioClip clip)
    {
        if (clip == menuMusic && menuMusicSource.isPlaying)
        {
            menuMusicSource.Stop();
        }

        if (clip == inGameMusic && inGameMusicSource.isPlaying)
        {
            inGameMusicSource.Stop();
        }
    }

    public void PauseMusic(AudioClip clip)
    {
        if (clip == menuMusic && menuMusicSource.isPlaying)
        {
            menuMusicSource.Pause();
        }

        if (clip == inGameMusic && inGameMusicSource.isPlaying)
        {
            inGameMusicSource.Pause();
        }
    }
}
