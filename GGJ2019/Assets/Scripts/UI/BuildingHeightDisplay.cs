using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class BuildingHeightDisplay : MonoBehaviour
    {
        [SerializeField]
        [Range(0.01f, 1)]
        private float _centerOffset;

        [SerializeField]
        private TextMeshProUGUI _text;

        [SerializeField]
        private Image _limitImage;

        private void Update()
        {
            var res = (Screen.height - Camera.main.WorldToScreenPoint(transform.position).y - (Screen.height * _centerOffset)) / (Screen.height);
            _text.color = new Color(_text.color.r, _text.color.g, _text.color.b, res);
            _limitImage.color = new Color(_limitImage.color.r, _limitImage.color.g, _limitImage.color.b, res);
        }

        public void SetHeightText(string value)
        {
            _text.text = value;
        }
    }
}