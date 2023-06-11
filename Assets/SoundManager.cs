using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioClip[] backgroundMusic;
    public AudioClip[] buttonSounds;

    private static int currentSong = 0;


    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource buttonSource;

    private void Awake()
    {
        SetSoundVolume(PlayerPrefs.GetFloat("Sound", 0.5f));
        SetMusicVolume(PlayerPrefs.GetFloat("Music", 0.5f));

        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(!musicSource.isPlaying) 
        {
            currentSong++;
            if (currentSong >= backgroundMusic.Length)
            {
                currentSong = 0;
            }
            musicSource.PlayOneShot(backgroundMusic[currentSong]);
        }
    }


    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public float GetSoundVolume()
    {
        return audioSource.volume;
    }

    public float GetMusicVolume()
    {
        return musicSource.volume;
    }

    public void SetSoundVolume(float vol)
    {
        audioSource.volume = vol;
        buttonSource.volume = vol;
    }
    public void SetMusicVolume(float vol)
    {
        musicSource.volume = vol;
    }

    public void PlayButtonClick()
    {
        buttonSource.PlayOneShot(buttonSounds[0]);
    }

    public void PlayTurretUpgrade()
    {
        buttonSource.PlayOneShot(buttonSounds[1]);
    }

    public void PlayTurretPickPlace()
    {
        buttonSource.PlayOneShot(buttonSounds[2]);
    }

    public void PlayTurretDestroy()
    {
        buttonSource.PlayOneShot(buttonSounds[3]);
    }

}
