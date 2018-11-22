using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CJooInputfield : MonoBehaviour 
{
    public InputField IDIF = null;
    public InputField PWIF = null;

    void OnEnable()
    {
        IDIF.text = "";
        PWIF.text = "";
    }
	
}
