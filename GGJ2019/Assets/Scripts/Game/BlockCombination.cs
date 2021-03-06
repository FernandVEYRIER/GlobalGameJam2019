﻿using Assets.Scripts.Blocks;
using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class BlockCombination : MonoBehaviour
    {
        [SerializeField]
        private Pair[] _combinationList;

        /// <summary>
        /// Checks if two elements are pairs.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public bool CheckPair(ABlock a, ABlock b)
        {
            var spriteA = a.GetComponent<SpriteRenderer>().sprite;
            var spriteB = b.GetComponent<SpriteRenderer>().sprite;

            foreach (Pair pair in _combinationList)
            {
                Sprite pairSprite = pair.GetPair(spriteA);

                if (pairSprite == spriteB)
                {
                    return true;
                }
            }

            return false;
        }
    }

    /// <summary>
    /// Represents a block pair.
    /// </summary>
    [Serializable]
    public class Pair
    {
        public Sprite Pair1;
        public Sprite Pair2;

        public Sprite GetPair(Sprite s)
        {
            if (s != Pair1 && s != Pair2)
                return null;
            if (s == Pair1)
                return Pair2;
            return Pair1;
        }
    }
}