using GameJamStarterKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{

    [SerializeField] private AudioSource[] _versionThemes;

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

}
