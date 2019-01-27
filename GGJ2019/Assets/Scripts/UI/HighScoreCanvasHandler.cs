using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class HighScoreCanvasHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scorePrefab;

        private readonly List<GameObject> _scores = new List<GameObject>();

        private void OnEnable()
        {
            StartCoroutine(AddScoreRoutine());
        }

        private IEnumerator AddScoreRoutine()
        {
            var scores = DataSaver.GetValue<List<ScoreItem>>("Scores") ?? new List<ScoreItem>();

            scores.Sort((x, y) => y.Score.CompareTo(x.Score));
            _scores.ForEach(x => Destroy(x));
            _scores.Clear();
            for (int i = 0; i < scores.Count; i++)
            {
                var go = Instantiate(_scorePrefab);
                go.transform.SetParent(transform);
                go.transform.localScale = Vector3.one;
                go.GetComponent<ScoreElement>().SetName(scores[i].Name).SetScore(scores[i].Score).SetRank(i + 1);
                _scores.Add(go);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}