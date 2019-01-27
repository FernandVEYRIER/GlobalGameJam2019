﻿using Assets.Scripts.Game;
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
            if (_combinedBlockInstance.CheckCombination())
            {
                _constructionHandler.PushBlock(_combinedBlockInstance);
                CreateBlockInstance();
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