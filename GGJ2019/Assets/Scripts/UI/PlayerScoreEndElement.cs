using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerScoreEndElement : MonoBehaviour
    {
        [SerializeField]
        private RectTransform _separator;

        [SerializeField]
        private TextMeshProUGUI _textUI;

        public bool IsSeparator
        {
            get => _isSeparator;
            set
            {
                _isSeparator = value;
                _separator.gameObject.SetActive(value);
                _textUI.gameObject.SetActive(!value);
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                _text = value;
                _textUI.text = value;
            }
        }

        private bool _isSeparator;
        private string _text;
    }
}
