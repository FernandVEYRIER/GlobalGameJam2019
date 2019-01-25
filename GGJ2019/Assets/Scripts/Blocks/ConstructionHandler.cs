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

        /// <summary>
        /// List of blocks in the building.
        /// </summary>
        private readonly List<IBlock> _blocks = new List<IBlock>();

        /// <summary>
        /// Pushes a block in the building.
        /// </summary>
        /// <param name="block"></param>
        /// <returns>The total block count</returns>
        public int PushBlock(IBlock block)
        {
            _blocks.Add(block);
            return _blocks.Count;
        }
    }
}