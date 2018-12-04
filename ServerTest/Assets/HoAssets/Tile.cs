using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public float m_FadeDuration = 0.5f;
    public float m_FadeOutScale = 1.5f;
    public float m_BlinkDelay = 2.0f;
    public float m_MissDelay = 0.25f;
	public float m_HintBlink = 2f;
    [HideInInspector]
    public int m_Num;
    [HideInInspector]
    public TMPro.TMP_Text m_NumText;
    [HideInInspector]
    public SpriteRenderer m_Sr;
    Transform m_Tf;
    Collider2D m_Col;
    Vector3 m_OriScale;
    bool m_IsPlaying;
    bool m_IsDestroy = false;
    private void Awake()
    {
        m_Col = GetComponent<Collider2D>();
        m_Sr = GetComponent<SpriteRenderer>();
        m_NumText = GetComponentInChildren<TMPro.TMP_Text>();
        m_Tf = transform;
        m_OriScale = m_Tf.localScale;
    }
    public bool IsDestory()
    {
        return m_IsDestroy;
    }
    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine("Co_FadeIn");
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine("Co_FadeOut");
    }
    //public void Blink()
    //{
	//	if (csGameData.GetInstance ().IsClickHintSkill == false) {
	//		StopAllCoroutines ();
	//		StartCoroutine ("Co_Blink");
	//	} else {
	//		StopAllCoroutines ();
	//		StartCoroutine("Co_HintItem");
	//	}
    //}

	public void Blink()
	{
		
		StopAllCoroutines ();
		StartCoroutine ("Co_Blink");

	}
	public void HintBlink()
	{
		StopAllCoroutines ();
		StartCoroutine("Co_HintItem");
	}
    public void Miss()
    {
        //StopAllCoroutines();
        StartCoroutine("Co_Miss");
    }
	//public void HintItem()
	//{
	//	StopCoroutine ();
	//	StartCoroutine("Co_HintItem");
	//}
    IEnumerator Co_FadeIn()
    {
        m_IsDestroy = false;
        m_IsPlaying = true;
        //m_Col.enabled = true;
        m_Sr.color = Color.black;
        m_Tf.localScale = Vector3.zero;
        m_NumText.color = Color.white;
        float t = 0;
        yield return null;

        while (true)
        {
            t += Time.deltaTime / m_FadeDuration;

            m_Tf.localScale = Vector3.Lerp(Vector3.zero, m_OriScale, t);
            if (t > 1)
                break;
            yield return null;
        }
        m_IsPlaying = false;
    }
    IEnumerator Co_FadeOut()
    {
        m_IsPlaying = true;
        m_IsDestroy= true;
        //m_Col.enabled = false;
        Vector3 destScale = m_OriScale * 1.5f;
        m_Tf.localScale = m_OriScale;
        float t = 0;
        yield return null;

        while (true)
        {
            t += Time.deltaTime / m_FadeDuration;

            m_Tf.localScale = Vector3.Lerp(m_OriScale, destScale, t);
            m_Sr.color = Color.Lerp(Color.black, Color.clear, t);
            m_NumText.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), t);

            if (t > 1)
                break;
            yield return null;
        }
        m_IsPlaying = false;
   
    }
    IEnumerator Co_Blink()
    {
        m_IsPlaying = true;
        WaitForSeconds wait = new WaitForSeconds(m_BlinkDelay);
        while (true)
        {
            m_Sr.color = Color.grey;
            m_NumText.color = Color.black;
			Debug.Log ("깜박임_01");
			yield return wait;
            m_Sr.color = Color.black;
            m_NumText.color = Color.white;
			Debug.Log ("깜박임_02");
			yield return wait;
        }
    }
    IEnumerator Co_Miss()
    {
        m_IsPlaying = true;
        WaitForSeconds wait = new WaitForSeconds(m_MissDelay);

        while (true)
        {
            m_Sr.color = Color.red;
            yield return wait;
            m_Sr.color = Color.black;
            yield break;
        }
    }
    public bool IsPlaying()
    {
        return m_IsPlaying;
    }

	IEnumerator Co_HintItem()
	{
    
        m_IsPlaying = true;
		WaitForSeconds wait = new WaitForSeconds (m_HintBlink);
		while (true) {
            m_Sr.color = csItemMgr.GetInstance ().UseGodOfHint_SpritetRender (m_Sr,Color.grey);
			m_NumText.color =  csItemMgr.GetInstance ().UseGodOfHint_Text (m_NumText,Color.black);
			yield return wait;
			m_Sr.color = csItemMgr.GetInstance ().UseGodOfHint_SpritetRender (m_Sr,Color.black);
			m_NumText.color =  csItemMgr.GetInstance ().UseGodOfHint_Text (m_NumText,Color.white);
			yield return wait;
		}

	}
}
