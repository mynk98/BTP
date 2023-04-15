using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MasterController : MonoBehaviour
{
    public static MasterController get;

    [Header("[AUDIO CONFIG]")]
    public bool IsMusic;
    public bool IsSound;
    [Space]
    public AudioSource MusicSource;
    public float MusicVolume = 0.5f;
    public AudioSource SfxSource;
    [Header("[AUDIO CONFIG - PRESETS]")]
    public List<AudioClipper> PresetAudioClips;
    public AudioClip MusicClip;
    [Space]
    public int QualityLevel;
    public bool FullScreen;

    private void Awake()
    {
        get = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        MusicSource.volume = MusicVolume;
    }

    void Update()
    {
        if (MusicSource.volume != MusicVolume)
        {
            MusicSource.volume = MusicVolume;
        }
    }

    public void PlayPresetSfx(int SfxPreset)
    {
        SfxPreset  -= 1;
        if (SfxPreset < 0) return;
        if (!IsSound) return;
        SfxSource.PlayOneShot(PresetAudioClips[SfxPreset].Clip);
    }

    public void PlaySfx(AudioClip SfxClip)
    {
        if (!IsSound) return;
        SfxSource.PlayOneShot(SfxClip);
    }

    public void AssignMusicClip(AudioClip Clip)
    {
        MusicClip = Clip;
        MusicSource.clip = MusicClip;
        MusicSource.loop = true;
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (!IsMusic) return;
        if (MusicSource.clip==null) return;

        MusicSource.Play();
    }

    public void StopMusic()
    {
        MusicSource.Stop();
    }

    IEnumerator IStopMusic()
    {
        MusicSource.DOFade(0, 1);
        yield return new WaitForSeconds(1);
        MusicSource.Stop();
    }

    public void ToggleSound(bool isOn)
    {
        IsSound = isOn;
    }

    public void ToggleMusic(bool isOn)
    {
        IsMusic = isOn;
        if (IsMusic) PlayMusic();
        if (!IsMusic) StopMusic();
    }

    public void ToggleFullScreen()
    {
        if(FullScreen)
        {

        }
        else
        {

        }
    }

    public void UpdateMusicVolume(float Value)
    {
        MusicVolume = Value;
        MusicSource.volume = MusicVolume;
    }

    public void SetQuality(int Index)
    {
        QualityLevel = Index;
    }

    [Serializable]
    public class AudioClipper
    {
        public string Title;
        public AudioClip Clip;
    }
}

public enum SfxPresets
{
    None,
    Intro,
    RoundButton,
    StandardButton,
    Popup1,
    Popup2,
    Switch,
    Happy1,
    Happy2,
    Happy3,
    Win1,
    Win2,
    Lose1,
    Lose2,
    Coins1,
    Coins2,
    PowerUp1,
    PowerUp2
}
