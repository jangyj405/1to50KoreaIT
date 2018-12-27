using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BackEnd;

public class OptionView : MonoBehaviour {

    public GameObject LogOutPanel;
    public Slider backgroundVolumeSlider;
    public Slider effectVolumeSlider;
    private float backgroundVolumeValue = 1f;
    private float effectVolumeValue = 1f;

   

    public void BackgroundSoundSlider()
    {        
        backgroundVolumeValue = backgroundVolumeSlider.value;
        PlayerPrefs.SetFloat("backgroundVolume", backgroundVolumeValue);
        SoundManager.instance.BGMAudioSource.volume = backgroundVolumeValue;       
    }

    public void EffectSoundSlider()
    {        
        effectVolumeValue = effectVolumeSlider.value;
        PlayerPrefs.SetFloat("effectVolume", effectVolumeValue);
        SoundManager.instance.SFXAudioSource.volume = effectVolumeValue;
    }

    public void CloseClick()
    {
        if (ModeSelect.stageModeKind == StageModeKind.StageMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            FadeInOut.instance.FadeIn(SceneNames.timeAttackScene);
        }
    }

    public void NickNameChangeClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.nicknameChangeScene);
    }

    public void LogOutClick()
    {
        LogOutPanel.SetActive(true);        
    }

    public void LogOutWithdrawAfterConfirmClick()
    {
        Backend.BMember.Logout();
        PlayerPrefs.DeleteAll();
        FadeInOut.instance.FadeIn(SceneNames.AccountScene);
        
    }


}
