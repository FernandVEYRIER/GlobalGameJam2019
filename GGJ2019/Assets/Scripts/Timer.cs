using TMPro;
using UnityEngine;

namespace Assets.Scripts.Game
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class Timer : MonoBehaviour
    {
        private TextMeshProUGUI _timerText;

        private void Start()
        {
            _timerText = GetComponent<TextMeshProUGUI>();
        }

        private void Update()
        {
            int minutes = Mathf.FloorToInt(GameManager.Instance.Timer / 60F);
            int seconds = Mathf.FloorToInt(GameManager.Instance.Timer - minutes * 60);
            _timerText.text = $"{minutes:0}:{seconds:00}";
        }
    }
}