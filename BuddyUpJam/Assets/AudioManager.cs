using GameJamStarterKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private AudioSource[] _versionThemes;
    [SerializeField] private AudioSource _walkingAudio;
    [SerializeField] private AudioSource _soundEffect;

    [SerializeField] private AudioClip _openCloseStoryBookClip;
    [SerializeField] private AudioClip _useWandClip;
    [SerializeField] private AudioClip _openDoorClip;
    [SerializeField] private AudioClip _shrinkEnlargeClip;
    [SerializeField] private AudioClip _unlockDoorClip;

    private bool IsPlaying;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayVersionTrack(int version)
    {

        _versionThemes.ForEach(f => f.mute = true);
        _versionThemes[version].mute = false;

        if (!IsPlaying)
        { 
            //_versionThemes.ForEach(f => f.Play());
            _versionThemes.ForEach(f => f.FadeIn(2));
            IsPlaying = true;
        }

    }

    public void StopPlaying()
    {
        _versionThemes.ForEach(f => f.FadeOut(1f));
    }

    public void PlayWalkingAudio()
    {
        _walkingAudio.Play();

    }

    public void PauseWalkingAudio()
    {
        if (_walkingAudio.isPlaying)
        {
            _walkingAudio.Stop();
        }
    }


    public void PlayStorybookOpenCloseClip()
    {
        _soundEffect.PlayOneShot(_openCloseStoryBookClip);
    }

    public void PlayUseWandClip()
    {
        _soundEffect.PlayOneShot(_useWandClip);
    }

    public void PlayShrinkEnlargeClip()
    {
        _soundEffect.PlayOneShot(_shrinkEnlargeClip);
    }

    public void PlayOpenDoorClip()
    {
        _soundEffect.PlayOneShot(_openDoorClip);
    }

    public void PlayUnlockDoorClip()
    {
        _soundEffect.PlayOneShot(_unlockDoorClip);
    }

    public void PlayClip(AudioClip clip)
    {
        _soundEffect.PlayOneShot(clip);
    }

}
