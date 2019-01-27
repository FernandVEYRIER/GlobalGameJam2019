using Assets.Scripts.Game;
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

        [SerializeField]
        private Animator _animator;

        private void Start()
        {
            CreateBlockInstance();
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
            var res = _combinedBlockInstance.CheckCombination(out bool isNull);
            if (res)
            {
                _animator.SetTrigger("Good");
                _constructionHandler.PushBlock(_combinedBlockInstance);
                CreateBlockInstance();
            }
            else if (!isNull)
            {
                _animator.SetTrigger("Bad");
            }
        }

        private void CreateBlockInstance()
        {
            _combinedBlockInstance = Instantiate(_combinedBlockPrefab);
            _combinedBlockInstance.transform.SetParent(transform);
            _combinedBlockInstance.transform.localPosition = Vector3.zero;
        }
    }
}