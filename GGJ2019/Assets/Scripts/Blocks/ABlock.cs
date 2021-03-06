﻿using UnityEngine;

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

        public virtual void SetBlockPosition(Vector3 pos)
        {
            transform.localPosition = pos;
        }

        public virtual void DestroyBlock()
        {
            Destroy(gameObject);
        }
    }
}