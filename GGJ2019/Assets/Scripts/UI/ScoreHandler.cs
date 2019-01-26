using Assets.Scripts.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class ScoreHandler : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scorePrefab;

        private readonly List<GameObject> _scores = new List<GameObject>();

        void Awake()
        {
            var l = new List<ScoreItem>();
            l.Add(new ScoreItem { Name = "John Doe", Score = 42 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            l.Add(new ScoreItem { Name = "Bob Lepicoleur", Score = 666 });
            DataSaver.SetValue("Scores", l);
            DataSaver.SaveData();
        }

        private void OnEnable()
        {
            StartCoroutine(AddScoreRoutine());
        }

        private IEnumerator AddScoreRoutine()
        {
            var scores = DataSaver.GetValue<List<ScoreItem>>("Scores") ?? new List<ScoreItem>();

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