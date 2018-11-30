using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendView : MonoBehaviour {

    public GameObject friendAddScreen;
    public Transform friendContent;
    public int friendCount;

    public InputField nickNameInputField;
    public GameObject friendList;

    public void Start()
    {
        friendCount = friendContent.transform.childCount;        
    }

    public void FriendAddClick()
    {

        friendAddScreen.SetActive(true);
    }

    public void CloseClick()
    {
        if (ModeSelect.stageModeKind == StageModeKind.StageMode)
        {
            SceneManager.LoadScene(SceneNames.stageScene);
        }
        else if (ModeSelect.stageModeKind == StageModeKind.TimeAttackMode)
        {
            SceneManager.LoadScene(SceneNames.timeAttackScene);
        }
    }

    public void FriendAddReal()
    {
        
    }

    public void FriendAddScreenCloseClick()
    {
        friendAddScreen.SetActive(false);
    }
}
