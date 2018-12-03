using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Friend : MonoBehaviour {

    public Text txtNickName;
    protected string inDate = "";
    public string InDate
    {
        get
        {
            return inDate;
        }
    }

    protected string nickName;
    public string NickName
    {
        get
        {
            return nickName;
        }
        private set
        {
            nickName = value;
            txtNickName.text = nickName;
        }
    }

    public void InitialOneFriend(string pNickname, string pInDate)
    {
        NickName = pNickname;
        inDate = pInDate;
    }
}
