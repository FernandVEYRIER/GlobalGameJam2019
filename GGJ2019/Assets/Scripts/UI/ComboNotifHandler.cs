using Assets.Scripts.Game;
using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ComboNotifHandler : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private TextMeshProUGUI _textCombo;

        private void Start()
        {
            GameManager.Instance.ScoreHandler.OnComboChange += ScoreHandler_OnComboChange;
        }

        private void ScoreHandler_OnComboChange(object sender, Data.ComboEvent e)
        {
            if (e.CurrentCombo > 1)
            {
                _textCombo.text = "x" + e.CurrentCombo.ToString();
                _animator.SetTrigger("ComboChange");
            }
        }

        private void Destroy()
        {
            GameManager.Instance.ScoreHandler.OnComboChange -= ScoreHandler_OnComboChange;
        }
    }
}