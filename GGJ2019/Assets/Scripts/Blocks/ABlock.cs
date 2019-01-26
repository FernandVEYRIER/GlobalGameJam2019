using UnityEngine;

namespace Assets.Scripts.Blocks
{
    public abstract class ABlock : MonoBehaviour, IBlock
    {
        public abstract float GetHeight();

        public abstract float GetWidth();
    }
}