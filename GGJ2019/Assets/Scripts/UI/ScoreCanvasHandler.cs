using Assets.Scripts.Data;
using Assets.Scripts.Game;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoreCanvasHandler : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private TextMeshProUGUI _textCombo;

        private ScoreHandler _scoreHandler;

        private void Start()
        {
            _scoreHandler = GameManager.Instance.ScoreHandler;
            _scoreHandler.OnComboChange += _scoreHandler_OnComboChange;

            _text.text = "0";
            _textCombo.text = "";
        }

        private void _scoreHandler_OnComboChange(object sender, ComboEvent e)
        {
            if (e.CurrentCombo <= 1)
                _textCombo.text = "";
            else
                _textCombo.text = "x" + ((int)e.CurrentCombo).ToString();
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == GameManager.State.PLAY)
            {
                _text.text = ((int)_scoreHandler.Score).ToString();
            }
        }

        private void OnDestroy()
        {
            _scoreHandler.OnComboChange -= _scoreHandler_OnComboChange;
        }
    }
}