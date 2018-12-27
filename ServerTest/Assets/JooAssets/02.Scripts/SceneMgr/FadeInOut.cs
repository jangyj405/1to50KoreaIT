using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour {

    public static FadeInOut instance = null;
    public Image fadeInOutImage;
    public GameObject canvasObject;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        canvasObject.SetActive(false);
        DontDestroyOnLoad(gameObject);
    }

    public void FadeIn(string sceneName)
    {
        canvasObject.SetActive(true);
        Tween tTween = fadeInOutImage.DOFade(1f, 1.2f);
        tTween.OnComplete(() =>
        {           
            SceneManager.LoadScene(sceneName);
            FadeOut();
        });
    }
    private void FadeOut()
    {
        Tween tTween = fadeInOutImage.DOFade(0f, 1.2f);
        tTween.OnComplete(() =>
        {
            canvasObject.SetActive(false);
        });        
    }

    public void FadeInReStart()
    {
        canvasObject.SetActive(true);
        Tween tTween = fadeInOutImage.DOFade(1f, 1.2f);
        tTween.OnComplete(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            FadeOutReStart();
        });
    }
    private void FadeOutReStart()
    {
        Tween tTween = fadeInOutImage.DOFade(0f, 1.2f);
        tTween.OnComplete(() =>
        {
            canvasObject.SetActive(false);
        });
    }
}
