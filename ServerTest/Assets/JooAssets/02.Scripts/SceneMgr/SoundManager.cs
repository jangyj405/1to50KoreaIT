using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public static SoundManager instance = null;
    public AudioSource BGMAudioSource;
    public AudioSource SFXAudioSource;

    public AudioClip BGMMainMenuAudioClip;
    public AudioClip[] BGMGameSceneAudioClip;

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
        MainMenuBGM();
    }

    public void MainMenuBGM()
    {
        BGMAudioSource.clip = BGMMainMenuAudioClip;
        BGMAudioSource.Play();
    }

    public void GameSceneBGM()
    {
        int index = Random.Range(0, BGMGameSceneAudioClip.Length);
        BGMAudioSource.clip = BGMGameSceneAudioClip[index];
        BGMAudioSource.Play();
    }
}
