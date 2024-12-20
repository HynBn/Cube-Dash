using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    [Header("Audiomixer")]
    [SerializeField] private AudioMixer m_Mixer;

    [Header("Slider")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        m_Mixer.SetFloat("Music", Mathf.Log10(volume) * 20);    //recalculated from linear to logarithmic 
    }

    public void SetSFXVolume()
    {
        float volume = sfxSlider.value;
        m_Mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}
