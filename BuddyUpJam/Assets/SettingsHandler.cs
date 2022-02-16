using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsHandler : MonoBehaviour
{

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicSlider;
    [SerializeField] private Slider _effectSlider;

    private void Start()
    {
        
        _audioMixer.GetFloat("MasterVolume", out float volume);
        _musicSlider.value = volume;
        _audioMixer.GetFloat("EffectsVolume", out volume);
        _effectSlider.value = volume;

    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        _audioMixer.SetFloat("EffectsVolume", volume);
    }


}
