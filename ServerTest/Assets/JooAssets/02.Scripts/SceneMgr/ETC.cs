﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ETC : MonoBehaviour {
    
    public void DiaPurchasClick()
    {
        SceneManager.LoadScene("DiaPurchas");
    }

    public void HeartPurchasClick()
    {
        SceneManager.LoadScene("HeartPurchas");
    }

    public void CharacterClick()
    {
        SceneManager.LoadScene("Character");
    }

    public void OptionClick()
    {
        SceneManager.LoadScene("Option");
    }

    public void PostClick()
    {
        SceneManager.LoadScene("Post");
    }

    public void FriendClick()
    {
        SceneManager.LoadScene("Friend");
    }

    public void ItemClick()
    {
        SceneManager.LoadScene("ItemPurchas");
    }

    public void CharacterPurchasClick()
    {
        SceneManager.LoadScene("CharacterPurchas");
    }
}
