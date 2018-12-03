using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IsSelect : MonoBehaviour
{
    public Button[] characterButton;
    public int index;
    
    

    public void CharacterClick(int pIndex)
    {
        index = pIndex;
        for (int i = 0; i < characterButton.Length; i++)
        {            
            characterButton[i].interactable = true;
        }
        characterButton[index].interactable = false;
        csCharacterMgr.GetInstance().CharacterChoice(index);
    }
}
