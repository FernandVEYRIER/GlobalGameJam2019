using Assets.Scripts.Game;
using System.Collections;
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

        [SerializeField]
        private float _blockMoveSpeed = 1f;

        [SerializeField]
        private Animator _animator;

        [SerializeField]
        private ParticleSystem _particleSystem;

        private float currTime;
        private Vector3 startPos;

        private void Start()
        {
            startPos = transform.position;
            var em = _particleSystem.emission;
            em.enabled = false;
        }

        public void PushBlockLeft(ABlock block)
        {
            if (_leftBlock)
            {
                Replace(_leftBlock, 1f);
            }
            _leftBlock = block;
            _leftBlock.transform.SetParent(transform);
            _leftBlock.transform.localPosition = Vector3.zero;
        }

        public void PushBlockRight(ABlock block)
        {
            if (_rightBlock)
            {
                Replace(_rightBlock, -1f);
            }
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
            StartCoroutine(FadeCoroutine(true));
        }

        public void Update()
        {
            if (_isTargetSet)
            {
                transform.localPosition = Vector3.Lerp(startPos, _target, currTime += Time.deltaTime * _blockMoveSpeed);
                if (Vector3.Distance(transform.localPosition, _target) <= 0.01f)
                {
                    StartCoroutine(FadeCoroutine(false));
                    _isTargetSet = false;
                    transform.localPosition = _target;
                }
            }
        }

        private IEnumerator FadeCoroutine(bool fadeOut)
        {
            if (fadeOut)
            {
                var em = _particleSystem.emission;
                em.enabled = true;
                //for (float i = 1; i >= 0; i -= 1f)
                //{
                //    foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                //    {
                //        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, i);
                //    }
                //    yield return new WaitForSeconds(0.01f);
                //}
                foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0);
                }
            }
            else
            {
                var em = _particleSystem.emission;
                em.enabled = false;
                for (float i = 0; i <= 1; i += 0.1f)
                {
                    if (i > 1) i = 1;
                    foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                    {
                        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, i);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                foreach (var sprite in GetComponentsInChildren<SpriteRenderer>())
                {
                    sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
                }
            }
        }

        private void Replace(ABlock block, float dir)
        {
            var rb = block.GetComponent<Rigidbody2D>();
            block.GetComponent<Collider2D>().enabled = true;
            block.GetComponent<SpriteRenderer>().sortingOrder = 100;
            rb.bodyType = RigidbodyType2D.Dynamic;
            rb.AddTorque(100f * dir, ForceMode2D.Impulse);
            StartCoroutine("ScaleUp", block);
        }

        private IEnumerator ScaleUp(ABlock block)
        {
            var maxSize = block.transform.localScale * 2f;
            while (block != null && maxSize.x > block.transform.localScale.x)
            {
                block.transform.localScale = Vector3.Slerp(block.transform.localScale, maxSize, 10f * Time.deltaTime);
                yield return 0;
            }
        }
    }
}