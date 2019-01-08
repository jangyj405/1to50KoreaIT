using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using BackEnd;

public class ETC : MonoBehaviour
{
	public Text nickNameText = null;
	public Text diaText = null;
	void Start()
	{
		SetNickname();
		diaText.text = CJooDiaCounter.GetTBCAmount().ToString();
	}

	private void SetNickname()
	{
		BackendReturnObject bro = Backend.BMember.GetUserInfo();
		string NickName = bro.GetReturnValuetoJSON()["row"]["nickname"].ToString();
		nickNameText.text = NickName;
		nickNameText.color = (ModeSelect.stageModeKind == StageModeKind.StageMode) ? Color.black : Color.white;
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
