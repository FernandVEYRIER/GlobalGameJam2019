using UnityEngine;

namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Abstract class for blocks.
    /// </summary>
    public abstract class ABlock : MonoBehaviour, IBlock
    {
        public abstract float GetHeight();

        public abstract float GetWidth();
    }
}