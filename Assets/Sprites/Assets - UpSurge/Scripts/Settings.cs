using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public Toggle Music;
    public Slider MusicVolumeSlider;
    public Toggle Sound;
    public Toggle QualityHigh;
    public Toggle QualityMedium;
    public Toggle QualityLow;
    public Toggle FullScreen;

    void Start()
    {
        Music.isOn = MasterController.get.IsMusic;
        Sound.isOn = MasterController.get.IsSound;
        MusicVolumeSlider.value = MasterController.get.MusicVolume;
        FullScreen.isOn = Screen.fullScreen;
    }

    private void Update()
    {
        if (MasterController.get.MusicVolume != MusicVolumeSlider.value)
        {
            MasterController.get.MusicVolume = MusicVolumeSlider.value;
        }
        //if (MasterController.get.MusicVolume > 0.0f) Music.isOn = true;
        //if (MasterController.get.MusicVolume == 0.0f) Music.isOn = false;
    }

    private void OnEnable()
    {
        Music.isOn = MasterController.get.IsMusic;
        Sound.isOn = MasterController.get.IsSound;

        if(MasterController.get.QualityLevel==0)
        {
            QualityHigh.isOn = true;
        }
        if (MasterController.get.QualityLevel == 1)
        {
            QualityMedium.isOn = true;
        }
        if (MasterController.get.QualityLevel == 2)
        {
            QualityLow.isOn = true;
        }
        FullScreen.isOn = Screen.fullScreen;
    }

    public void SetMusic()
    {
        if (MasterController.get.IsMusic != Music.isOn) MasterController.get.ToggleMusic(Music.isOn);
    }

    public void SetSound()
    {
        if(MasterController.get.IsSound!= Sound.isOn) MasterController.get.ToggleSound(Sound.isOn);
    }

    public void FullScreenOn()
    {
        FullScreen.isOn = true;
    }
    public void FullScreenOff()
    {
        FullScreen.isOn = false;
    }

    public void SetQuality()
    {
        int QualityNow = 0;
        if (QualityHigh.isOn) QualityNow = 0;
        if (QualityMedium.isOn) QualityNow = 1;
        if (QualityLow.isOn) QualityNow = 2;

        if (MasterController.get.QualityLevel != QualityNow)
        {
            MasterController.get.SetQuality(QualityNow);
        }
    }

    public void SetMusicVolume(Slider Volume)
    {
        MasterController.get.UpdateMusicVolume(Volume.value);
    }
}
