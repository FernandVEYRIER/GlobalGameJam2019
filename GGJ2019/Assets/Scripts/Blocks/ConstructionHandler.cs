using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Handles the construction of the building.
    /// </summary>
    public class ConstructionHandler : MonoBehaviour
    {
        [Tooltip("Width of the building block count.")]
        [SerializeField]
        private int _width = 1;

        [Tooltip("Scale converter for block height display.")]
        [SerializeField]
        private int _blockScale = 1;

        private Vector3 _startPosition;

        private float _height = 0;

        /// <summary>
        /// List of blocks in the building.
        /// </summary>
        private readonly List<ABlock> _blocks = new List<ABlock>();

        void Start()
        {
            _startPosition = transform.localPosition;
        }

        /// <summary>
        /// Pushes a block in the building.
        /// </summary>
        /// <param name="block"></param>
        /// <returns>The total block count</returns>
        public int PushBlock(ABlock block)
        {
            block.transform.SetParent(transform);
            for (int i = 0; i < block.transform.childCount; ++i)
            {
                block.transform.GetChild(i).transform.localScale = Vector3.one;
            }
            block.SetBlockPosition(new Vector3(
                /*_startPosition.x +*/ (block.GetWidth() * (_blocks.Count % _width)),
                /*_startPosition.y +*/ (block.GetHeight() * (_blocks.Count / _width)),
                _startPosition.z));
            //block.transform.localPosition = new Vector3(
            //    _startPosition.x + (block.GetWidth() * (_blocks.Count % _width)),
            //    _startPosition.y + (block.GetHeight() * (_blocks.Count / _width)),
            //    _startPosition.z);
            _blocks.Add(block);
            if (_blocks.Count % _width == 0)
            {
                _height += block.GetHeight();
            }
            return _blocks.Count;
        }

        /// <summary>
        /// Gets the blocks total height.
        /// </summary>
        /// <returns></returns>
        public float GetBlockTotalHeight()
        {
            return _height;
        }

        public float GetBlockTotalHeightWithScaling()
        {
            return GetBlockTotalHeight() * _blockScale;
        }
    }
}