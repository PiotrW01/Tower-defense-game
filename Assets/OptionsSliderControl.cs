using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsSliderControl : MonoBehaviour
{
    public Slider soundSlider;
    public Slider musicSlider;

    private void OnEnable()
    {
        soundSlider.value = SoundManager.Instance.GetSoundVolume();
        musicSlider.value = SoundManager.Instance.GetMusicVolume();
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
