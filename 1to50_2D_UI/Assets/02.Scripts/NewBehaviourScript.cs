using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript
{
    IEnumerator asdf()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("asdf");
        
    }

}
