using Assets.Scripts.Blocks;
using Assets.Scripts.Data;
using System;
using UnityEngine;

namespace Assets.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public enum State { PLAY, PAUSE, GAME_OVER }

        public ScoreHandler ScoreHandler => _scoreHandler;

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

        [SerializeField]
        private ScoreHandler _scoreHandler;
        
        [SerializeField]
        private float _scoreStep = 1;

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

        private void Update()
        {
            if (GameState == State.PLAY)
            {
                ScoreHandler.AddPoints(Time.deltaTime * _scoreStep);
            }
        }

        public bool CheckCombination(ABlock a, ABlock b)
        {
            if (_blockCombination.CheckPair(a, b))
            {
                ScoreHandler.SetPositiveAction(true);
                return true;
            }
            else
            {
                ScoreHandler.SetPositiveAction(false);
                a.DestroyBlock();
                b.DestroyBlock();
                return false;
            }
        }
    }

    public class GameEventArgs : EventArgs
    {
        public GameManager.State Current;
        public GameManager.State Previous;
    }
}