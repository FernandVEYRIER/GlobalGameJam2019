using Assets.Scripts.Blocks;
using Assets.Scripts.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Game
{
    public class GameManager : MonoBehaviour
    {
        public AudioClip success;
        public AudioClip fail;

        public enum State { COUNTDOWN, PLAY, PAUSE, GAME_OVER }

        public ScoreHandler ScoreHandler => _scoreHandler;

        public BlockBuilder BlockBuilder => _blockBuilder;

        public ConstructionHandler ConstructionHandler => _constructionHandler;

        public event EventHandler<GameEventArgs> OnGameStateChange;

        public UnityEvent OnTimerFinished;
        public UnityEvent OnTimerCloseToFinish;

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

        public int TotalScore { get; private set; }

        public float Timer => _timer;

        private State _gameState;

        [SerializeField]
        private BlockCombination _blockCombination;

        [SerializeField]
        private ScoreHandler _scoreHandler;
        
        [SerializeField]
        private float _scoreStep = 1;

        public static GameManager Instance { get; private set; }

        [SerializeField]
        private BlockBuilder _blockBuilder;

        [SerializeField]
        private ConstructionHandler _constructionHandler;

        private State _previousGameState;

        [SerializeField]
        private Cinemachine.CinemachineVirtualCamera _camera;

        [SerializeField]
        private float _timer = 90f;

        private bool isOver = false;

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

        private void Start()
        {
            OnTimerFinished.AddListener(() => GameState = State.GAME_OVER);
            OnGameStateChange += (e, v) =>
            {
                if (v.Current == State.GAME_OVER)
                {
                    TotalScore = (int)(ScoreHandler.Score + ConstructionHandler.GetBlockTotalHeight() * 1000);
                }
            };
            Invoke("StartGame", 3f);
        }

        private void StartGame()
        {
            GameState = State.PLAY;
        }

        private void Update()
        {
            if (GameState == State.PLAY)
            {
                ScoreHandler.AddPoints(Time.deltaTime * _scoreStep);
                if (_timer > 0)
                {
                    _timer -= Time.deltaTime;
                    if (_timer <= 10f && isOver == false)
                    {
                        isOver = true;
                        OnTimerCloseToFinish.Invoke();
                    }
                    if (_timer <= 0f)
                    {
                        _timer = 0f;
                        OnTimerFinished.Invoke();
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SetPause();
            }
        }

        public bool CheckCombination(ABlock a, ABlock b)
        {
            if (_blockCombination.CheckPair(a, b))
            {
                a.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                b.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
                ScoreHandler.SetPositiveAction(true);
                var audio = GetComponent<AudioSource>();
                audio.clip = success;
                audio.Play();
                return true;
            }
            else
            {
                ScoreHandler.SetPositiveAction(false);
                StopAllCoroutines();
                StartCoroutine(CameraShake(0.75f));
                a.DestroyBlock();
                b.DestroyBlock();
                var audio = GetComponent<AudioSource>();
                audio.clip = fail;
                audio.Play();
                return false;
            }
        }

        private IEnumerator CameraShake(float duration)
        {
            _camera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 4;
            yield return new WaitForSeconds(duration);
            _camera.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        }

        public void SetPause()
        {
            if (GameState != State.PAUSE)
            {
                _previousGameState = GameState;
                GameState = State.PAUSE;
                Time.timeScale = 0;
            }
            else
            {
                GameState = _previousGameState;
                Time.timeScale = 1;
            }
        }
    }

    public class GameEventArgs : EventArgs
    {
        public GameManager.State Current;
        public GameManager.State Previous;
    }
}