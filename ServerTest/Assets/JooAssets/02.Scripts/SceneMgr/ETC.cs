using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ETC : MonoBehaviour
{
	public Text diaText = null;
	void Start()
	{
		diaText.text = CJooDiaCounter.GetTBCAmount().ToString();
	}
    public void DiaPurchasClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.diaPurchaseScene);
    }

    public void HeartPurchasClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.heartPurchaseScene);
    }

    //public void CharacterClick()
    //{
    //    SceneManager.LoadScene("Character");
    //}

    public void OptionClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.optionScene);
    }

    public void PostClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.postScene);
    }

    public void FriendClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.friendScene);
    }

    public void ItemClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.itemPurchaseScene);
    }

    public void CharacterPurchasClick()
    {
        FadeInOut.instance.FadeIn(SceneNames.characterPurchaseScene);
    }
}
