using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliderControl : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;

    private void Start()
    {
        try
        {
            soundSlider.value = SoundManager.Instance.GetSoundVolume();
            musicSlider.value = SoundManager.Instance.GetMusicVolume();
        }
        catch { }
    }

    private void OnEnable()
    {
        try
        {
        soundSlider.value = SoundManager.Instance.GetSoundVolume();
        musicSlider.value = SoundManager.Instance.GetMusicVolume();
        }
        catch { }
    }

    public void ChangeSoundVolume()
    {
        SoundManager.Instance.SetSoundVolume(soundSlider.value);
    }

    public void ChangeMusicVolume()
    {
        SoundManager.Instance.SetMusicVolume(musicSlider.value);
    }

}
