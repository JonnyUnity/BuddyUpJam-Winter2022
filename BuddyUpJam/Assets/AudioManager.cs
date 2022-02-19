using GameJamStarterKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private AudioSource[] _versionThemes;
    [SerializeField] private AudioSource _soundEffect;

    [SerializeField] private AudioClip _openCloseStoryBookClip;
    [SerializeField] private AudioClip _useWandClip;
    [SerializeField] private AudioClip _shrinkEnlargeClip;
    [SerializeField] private AudioClip _CatClip;

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
            _versionThemes.ForEach(f => f.Play());
            IsPlaying = true;
        }

    }

    public void StopPlaying()
    {
        _versionThemes.ForEach(f => f.FadeOut(1f));
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

    public void PlayCatClip()
    {
        _soundEffect.PlayOneShot(_CatClip);
    }

}
