using UnityEngine;

namespace Assets.Scripts.Blocks
{
    /// <summary>
    /// Abstract class for blocks.
    /// </summary>
    public abstract class ABlock : MonoBehaviour, IBlock
    {
        public int currentID;
        public int targetID;

        public abstract float GetHeight();

        public abstract float GetWidth();
    }
}