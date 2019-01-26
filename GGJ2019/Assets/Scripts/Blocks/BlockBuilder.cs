using UnityEngine;

namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Builds blocks together.
    /// </summary>
    public class BlockBuilder : MonoBehaviour
    {
        [SerializeField]
        private CombinedBlock _combinedBlockPrefab;

        [SerializeField]
        private ConstructionHandler _constructionHandler;

        private CombinedBlock _combinedBlockInstance;

        public ABlock prefab;

        private void Start()
        {
            _combinedBlockInstance = Instantiate(_combinedBlockPrefab);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                PushBlockLeft(Instantiate(prefab));
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                PushBlockRight(Instantiate(prefab));
            }
        }

        public void PushBlockLeft(ABlock block)
        {
            _combinedBlockInstance.PushBlockLeft(block);
            CheckCombination();
        }

        public void PushBlockRight(ABlock block)
        {
            _combinedBlockInstance.PushBlockRight(block);
            CheckCombination();
        }

        private void CheckCombination()
        {
            if (_combinedBlockInstance.CheckCombination())
            {
                _constructionHandler.PushBlock(_combinedBlockInstance);
                _combinedBlockInstance = Instantiate(_combinedBlockPrefab);
            }
        }
    }
}