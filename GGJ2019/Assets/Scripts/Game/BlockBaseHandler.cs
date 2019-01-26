using Assets.Scripts.Blocks;
using Assets.Scripts.UI;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class BlockBaseHandler : MonoBehaviour
    {
        [SerializeField]
        private ConstructionHandler _constructionHandler;

        [SerializeField]
        private GameObject _heightDisplayPrefab;

        private Vector3 _target;

        [SerializeField]
        private float _smoothTime = 0.3f;

        [SerializeField]
        private int _heightStep = 10;
        [SerializeField]
        private int _heightForwardCount = 3;
        private int _lastHeightGenerated;

        private Vector3 _vel;

        private void Awake()
        {
            _target = transform.position;
        }

        private void Start()
        {
            SpawnHeightDisplay();
        }

        private void Update()
        {
            if (GameManager.Instance.GameState == GameManager.State.PLAY)
            {
                _target.y = -_constructionHandler.GetBlockTotalHeight();
                transform.position = Vector3.SmoothDamp(transform.position, _target, ref _vel, _smoothTime);

                if ((int)_constructionHandler.GetBlockTotalHeight() >= _lastHeightGenerated - _heightStep)
                {
                    SpawnHeightDisplay();
                }
            }
        }

        private void SpawnHeightDisplay()
        {
            for (int i = 0; i < _heightForwardCount; ++i)
            {
                var go = Instantiate(_heightDisplayPrefab);
                go.GetComponent<BuildingHeightDisplay>().SetHeightText(_lastHeightGenerated.ToString() + "m");
                go.transform.SetParent(transform);
                go.transform.localPosition = new Vector3(0, _lastHeightGenerated, 0);
                _lastHeightGenerated += _heightStep;
            }
        }
    }
}