using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityTools.SceneManagement;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateLoading : MonoBehaviour
{

    public string Format = "Loading {0}%";
    private TextMeshProUGUI m_T;

    private void Start()
    {
        m_T = GetComponent<TextMeshProUGUI>();
        SceneManager.Instance.LoadingEvent.AddListener(UpdateText);
    }

    private void UpdateText(float value)
    {
        m_T.text = string.Format(Format, value * 100);
    }

    private void OnDestroy()
    {
        SceneManager.Instance.LoadingEvent.RemoveListener(UpdateText);
    }

    public void UpdateColor(Color c)
    {
        if (gameObject.activeSelf)
            m_T.color = new Color(m_T.color.r, m_T.color.g, m_T.color.b, c.a);
    }
}
