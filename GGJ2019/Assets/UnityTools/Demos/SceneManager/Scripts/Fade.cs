using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTools.SceneManagement;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{

    private Image m_Img;
    [System.Serializable]
    public class ColorHandler : UnityEvent<Color>
    {

    }
    public ColorHandler OnColorChange;
    private enum FadeMode
    {
        In,
        Out
    }

    private float _xmax;

    private void Awake()
    {
        m_Img = GetComponent<Image>();
        m_Img.color = new Color(m_Img.color.r, m_Img.color.g, m_Img.color.b, 0);
        SceneManager.Instance.TransitionInEvent.AddListener(FadeTransition);
        SceneManager.Instance.TransitionOutEvent.AddListener(EndTransition);
        _xmax = m_Img.rectTransform.rect.xMax;
    }

    private void FadeTransition(float v)
    {
        Color c = m_Img.color;
        c.a = v;
        m_Img.color = c;
        OnColorChange.Invoke(c);
    }

    private void EndTransition(float v)
    {
        m_Img.rectTransform.localScale = new Vector3(v, m_Img.rectTransform.localScale.y, m_Img.rectTransform.localScale.z);
    }

    public void FadeIn()
    {
        StartCoroutine("AsyncFade", FadeMode.In);
    }

    public void FadeOut()
    {
        StartCoroutine("AsyncFade", FadeMode.Out);
    }

    IEnumerator AsyncFade(FadeMode value)
    {
        Color c = m_Img.color;
        switch (value)
        {
            case FadeMode.In:
                while (m_Img.color.a < 1)
                {
                    c.a += .01f;
                    m_Img.color = c;
                    OnColorChange.Invoke(c);
                    yield return new WaitForEndOfFrame();
                }
                break;
            case FadeMode.Out:
                while (m_Img.color.a > 0)
                {
                    c.a -= .01f;
                    m_Img.color = c;
                    OnColorChange.Invoke(c);
                    yield return new WaitForEndOfFrame();
                }
                break;
        }
    }
}
