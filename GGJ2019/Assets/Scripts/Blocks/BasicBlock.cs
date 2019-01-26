using UnityEngine;

namespace Assets.Scripts.Blocks
{
    public class BasicBlock : ABlock
    {
        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public override float GetHeight()
        {
            return _spriteRenderer.size.y;
        }

        public override float GetWidth()
        {
            return _spriteRenderer.size.x;
        }
    }
}