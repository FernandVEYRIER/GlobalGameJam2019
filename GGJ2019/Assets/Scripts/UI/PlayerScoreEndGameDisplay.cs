using Assets.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.UI
{
    public class PlayerScoreEndGameDisplay : MonoBehaviour
    {
        [SerializeField]
        private GameObject _scoreItemPrefab;

        private void OnEnable()
        {
            StopAllCoroutines();
            for (int i = 1; i < transform.childCount; ++i)
            {
                Destroy(transform.GetChild(i).gameObject);
            }
            StartCoroutine(DisplayElementsCoroutine());
        }

        private IEnumerator DisplayElementsCoroutine()
        {
            //var go = Instantiate(_scoreItemPrefab);
            //go.transform.SetParent(transform);
            //go.transform.localScale = Vector3.one;
            //go.GetComponent<PlayerScoreEndElement>().IsSeparator = false;
            //go.GetComponent<PlayerScoreEndElement>().Text = "Player1 & Player2";
            //yield return new WaitForSeconds(0.5f);

            var go = Instantiate(_scoreItemPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.GetComponent<PlayerScoreEndElement>().IsSeparator = false;
            go.GetComponent<PlayerScoreEndElement>().Text = ((int)GameManager.Instance.ConstructionHandler.GetBlockTotalHeight()).ToString() + " meters x1000";
            yield return new WaitForSeconds(0.5f);

            go = Instantiate(_scoreItemPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.GetComponent<PlayerScoreEndElement>().IsSeparator = false;
            go.GetComponent<PlayerScoreEndElement>().Text = ((int)GameManager.Instance.ScoreHandler.Score).ToString() + " pts";
            yield return new WaitForSeconds(0.5f);

            go = Instantiate(_scoreItemPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.GetComponent<PlayerScoreEndElement>().IsSeparator = true;

            go = Instantiate(_scoreItemPrefab);
            go.transform.SetParent(transform);
            go.transform.localScale = Vector3.one;
            go.GetComponent<PlayerScoreEndElement>().IsSeparator = false;
            go.GetComponent<PlayerScoreEndElement>().Text = "Total: "
                + GameManager.Instance.TotalScore.ToString()
                + " pts";
        }
    }
}