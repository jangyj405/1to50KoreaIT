using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JooScripts
{
    public class CJooTitleUI : MonoBehaviour
    {
        public GameObject panelSocialLogin = null;

        void Start()
        {
            CheckUserKey();
        }

        void CheckUserKey()
        {
            string tName = PlayerPrefs.GetString("UserName","");
            if(tName == "")
            {
                panelSocialLogin.SetActive(true);
            }
            else
            {
                panelSocialLogin.SetActive(false);
                Debug.Log(tName + " Login");
                StartCoroutine(ChangeScene());
            }
        }



        public void OnClickBtnSocialLogin(string pFlatform)
        {
            switch (pFlatform)
            {
                case "Facebook":
                case "Google":
                    {
                        LoginTemp();
                        CheckUserKey();
                        Debug.Log(pFlatform + " Login");
                    }
                    break;

                default:
                    {
                        Debug.Log("Error Social login");
                    }
                    break;
            }
        }

        void LoginTemp()
        {
            string tString = "장영주";
            PlayerPrefs.SetString("UserName", tString);
        }

        void OnGUI()
        {
            if(GUI.Button(new Rect(0f,0f, 100f, 50f),"ResetPlayerPrefs"))
            {
                PlayerPrefs.DeleteKey("UserName");
            }
        }

        IEnumerator ChangeScene()
        {
            yield return new WaitForSeconds(1f);
            SceneManager.LoadScene("02_mainmenu");
        }
    }
}