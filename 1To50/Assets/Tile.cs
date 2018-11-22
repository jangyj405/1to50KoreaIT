using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {
    public float m_FadeDuration = 0.5f;
    public float m_FadeOutScale = 1.5f;
    [HideInInspector]
    public int m_Num;
    [HideInInspector]
    public TMPro.TMP_Text m_NumText;
    Transform m_Tf;
    Vector3 m_OriScale;
    bool m_IsPlaying;
    private void Awake()
    {
        m_NumText = GetComponentInChildren<TMPro.TMP_Text>();
        m_Tf = transform;
        m_OriScale = m_Tf.localScale;
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
    IEnumerator Co_FadeIn()
    {
        m_IsPlaying = true;
        m_Tf.localScale = Vector3.zero;
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
        GetComponent<Collider2D>().enabled = false;
        Vector3 destScale = m_OriScale * 1.5f;
        m_Tf.localScale = m_OriScale;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        float t = 0;
        yield return null;

        while (true)
        {
            t += Time.deltaTime / m_FadeDuration;

            m_Tf.localScale = Vector3.Lerp(m_OriScale, destScale, t);
            sr.color = Color.Lerp(Color.black, Color.clear, t);
            m_NumText.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), t);

            if (t > 1)
                break;
            yield return null;
        }
        m_IsPlaying = false;
    }
    public bool IsPlaying()
    {
        return m_IsPlaying;
    }
}
