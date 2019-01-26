using System;
using UnityEngine;

namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Represents a fusion of two blocks.
    /// </summary>
    public class CombinedBlock : ABlock
    {
        private ABlock _leftBlock;
        private ABlock _rightBlock;

        public void PushBlockLeft(ABlock block)
        {
            _leftBlock = block;
            _leftBlock.transform.SetParent(transform);
            _leftBlock.transform.localPosition = Vector3.zero;
        }

        public void PushBlockRight(ABlock block)
        {
            _rightBlock = block;
            _rightBlock.transform.SetParent(transform);
            _rightBlock.transform.localPosition = Vector3.zero;
        }

        public bool CheckCombination()
        {
            if (_leftBlock == null || _rightBlock == null)
                return false;
            return true;
        }

        public override float GetHeight()
        {
            return _leftBlock?.GetHeight() ?? 0 + _rightBlock?.GetHeight() ?? 0;
        }

        public override float GetWidth()
        {
            return _leftBlock?.GetWidth() ?? 0 + _rightBlock?.GetWidth() ?? 0;
        }
    }
}