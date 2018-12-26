using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public AudioSource BGMAudioSource;
    public AudioSource SFXAudioSource;

    public AudioClip BGMMainMenuAudioClip;
    public AudioClip[] BGMGameSceneAudioClip;

    public AudioClip SFXCorrectClickAudioClip;
    public AudioClip SFXMissClickAudioClip;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        BGMAudioSource.volume = PlayerPrefs.GetFloat("backgroundVolume", 1f);
        SFXAudioSource.volume = PlayerPrefs.GetFloat("effectVolume", 1f);
        BGMMainMenu();
    }

    public void BGMMainMenu()
    {
        BGMAudioSource.clip = BGMMainMenuAudioClip;
        BGMAudioSource.Play();
    }

    public void BGMGameScene()
    {
        int index = Random.Range(0, BGMGameSceneAudioClip.Length);
        BGMAudioSource.clip = BGMGameSceneAudioClip[index];
        BGMAudioSource.Play();
    }

    public void SFXCorrectClick()
    {
        SFXAudioSource.clip = SFXCorrectClickAudioClip;
        SFXAudioSource.Play();
    }

    public void SFXMissClick()
    {
        SFXAudioSource.clip = SFXMissClickAudioClip;
        SFXAudioSource.Play();
    }
}
