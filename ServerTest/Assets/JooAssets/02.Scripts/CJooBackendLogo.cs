using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class CJooBackendLogo : MonoBehaviour 
{
    [SerializeField]
    private Image backendLogoImage = null;
	// Use this for initialization
	void Start ()
    {
		Screen.SetResolution(Screen.width, Screen.width * 16 / 9, true);
        Tween tTween = backendLogoImage.DOFade(0f, 2f).SetEase(Ease.OutSine).SetLoops(2, LoopType.Yoyo);
        tTween.OnComplete(() =>
            {
                SceneManager.LoadScene("02_AccountScene");
            });
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}

   
}
