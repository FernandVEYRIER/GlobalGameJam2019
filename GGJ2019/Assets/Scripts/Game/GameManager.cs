using Assets.Scripts.Blocks;
using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public enum State { PLAY, PAUSE, GAME_OVER }

        public event EventHandler<GameEventArgs> OnGameStateChange;

        public State GameState
        {
            get => _gameState;
            set
            {
                if (value != _gameState)
                {
                    OnGameStateChange?.Invoke(this, new GameEventArgs { Current = value, Previous = _gameState });
                    _gameState = value;
                }
            }
        }

        private State _gameState;

        [SerializeField]
        private BlockCombination _blockCombination;

        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        public bool CheckCombination(ABlock a, ABlock b)
        {
            return _blockCombination.CheckPair(a, b);
        }
    }

    public class GameEventArgs : EventArgs
    {
        public GameManager.State Current;
        public GameManager.State Previous;
    }
}