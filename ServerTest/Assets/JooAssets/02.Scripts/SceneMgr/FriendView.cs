using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FriendView : MonoBehaviour
{
    public static FriendView friendView = null;
    public GameObject friendAddScreen;
    public int friendCount;
    [Space(20)]
    public Transform friendContent;
    public Transform requestContent;

    public InputField nickNameInputField;
    public GameObject friendList;
    [Space(20)]
    public Friend friendPF;
    public CJooRequestedFriend requestedFriendPF;

    [Space(20)]
    public GameObject friendListPanel;
    public GameObject friendAcceptPanel;


    void Awake()
    {
        friendView = this;
    }


    public void Start()
    {
        friendCount = friendContent.transform.childCount;
        friendListPanel.SetActive(true);
        friendAcceptPanel.SetActive(false);
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


    public void FriendAddReal(string pNickName, string pInDate, bool hasSentHeart)
    {
        Friend instFriend = Instantiate<Friend>(friendPF, friendContent);
        instFriend.InitialOneFriend(pNickName, pInDate, hasSentHeart);
    }

    public void InstantiateRequestedList(string pNickName, string pInDate)
    {
        CJooRequestedFriend instRequest = Instantiate<CJooRequestedFriend>(requestedFriendPF, requestContent);
        instRequest.InitialOneFriend(pNickName, pInDate);
    }



    public void FriendAddScreenCloseClick()
    {
        friendAddScreen.SetActive(false);
    }

    public void FriendListClick()
    {
        friendListPanel.SetActive(true);
        friendAcceptPanel.SetActive(false);
    }

    public void FriendAcceptClick()
    {
        friendListPanel.SetActive(false);
        friendAcceptPanel.SetActive(true);
    }

}
