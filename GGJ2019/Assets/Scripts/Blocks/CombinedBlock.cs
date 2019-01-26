using Assets.Scripts.Game;
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

        private Vector3 _target;
        private bool _isTargetSet;

        private void Start()
        {
            startPos = transform.position;
        }

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
            return GameManager.Instance.CheckCombination(_leftBlock, _rightBlock);
        }

        public override float GetHeight()
        {
            return _leftBlock?.GetHeight() ?? 0 + _rightBlock?.GetHeight() ?? 0;
        }

        public override float GetWidth()
        {
            return _leftBlock?.GetWidth() ?? 0 + _rightBlock?.GetWidth() ?? 0;
        }

        public override void SetBlockPosition(Vector3 pos)
        {
            startPos = transform.localPosition;
            _target = pos;
            _isTargetSet = true;
        }

        private float currTime;
        private Vector3 startPos;

        public void Update()
        {
            if (_isTargetSet)
                transform.localPosition = Vector3.Lerp(startPos, _target, currTime += Time.deltaTime / 1.3f);
        }
    }
}