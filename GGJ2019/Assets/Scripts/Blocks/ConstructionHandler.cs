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

        private Vector3 _startPosition;

        public GameObject prefab;

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                var block = Instantiate(prefab);
                PushBlock(block.GetComponent<ABlock>());
            }
        }

        /// <summary>
        /// List of blocks in the building.
        /// </summary>
        private readonly List<ABlock> _blocks = new List<ABlock>();

        void Start()
        {
            _startPosition = transform.position;
        }

        /// <summary>
        /// Pushes a block in the building.
        /// </summary>
        /// <param name="block"></param>
        /// <returns>The total block count</returns>
        public int PushBlock(ABlock block)
        {
            block.transform.position = new Vector3(
                _startPosition.x + block.GetWidth() * (_blocks.Count % _width),
                _startPosition.y + block.GetHeight() * (_blocks.Count / _width),
                _startPosition.z);
            _blocks.Add(block);
            return _blocks.Count;
        }
    }
}