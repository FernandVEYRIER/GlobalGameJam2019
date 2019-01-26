using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoreElement : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;

        private ScoreItem _scoreItem = new ScoreItem();

        private int _rank;

        public ScoreElement SetName(string name)
        {
            _scoreItem.Name = name;
            UpdateUI();
            return this;
        }

        public ScoreElement SetScore(int score)
        {
            _scoreItem.Score = score;
            UpdateUI();
            return this;
        }

        public ScoreElement SetRank(int rank)
        {
            _rank = rank;
            UpdateUI();
            return this;
        }

        private void UpdateUI()
        {
            _text.text = $"{_rank}. {_scoreItem.Name} {_scoreItem.Score}pts";
        }
    }

    [Serializable]
    public class ScoreItem
    {
        public string Name;
        public int Score;
    }
}