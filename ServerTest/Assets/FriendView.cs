using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendView : MonoBehaviour {

    public GameObject friendAddScreen;
    public int friendCount;

    public Transform friendContent;

    public InputField nickNameInputField;
    public GameObject friendList;
    public Friend friendPF;
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
        //GameObject friend = (GameObject)Instantiate(friendList, friendContent.position, Quaternion.identity);
        //friend.transform.SetParent(friendContent);
        //friend.GetComponentInChildren<Text>().text = nickNameInputField.text;
        Friend instFriend = Instantiate<Friend>(friendPF, friendContent);
      //  instFriend.transform.SetParent(friendContent);
        instFriend.InitialOneFriend(nickNameInputField.text, "2018-11-11T08:00:00");
        //instFriend.NickName = "";
    }

    public void FriendAddScreenCloseClick()
    {
        friendAddScreen.SetActive(false);
    }


}
