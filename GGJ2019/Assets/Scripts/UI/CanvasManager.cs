using UnityEngine;
using UnityEngine.Events;
using UnityTools.SceneManagement;

namespace Assets.Scripts.Game
{
    public class CanvasManager : MonoBehaviour
    {
        public UnityEvent OnPlay;
        public UnityEvent OnPause;
        public UnityEvent OnGameOver;

        [SerializeField]
        private RectTransform panelGame;

        [SerializeField]
        private RectTransform panelGameOver;

        [SerializeField]
        private RectTransform panelPause;

        private void Start()
        {
            GameManager.Instance.OnGameStateChange += Instance_OnGameStateChange;
        }

        private void Instance_OnGameStateChange(object sender, GameEventArgs e)
        {
            switch (e.Current)
            {
                case GameManager.State.PLAY:
                    OnPlay.Invoke();
                    panelGame.gameObject.SetActive(true);
                    panelGameOver.gameObject.SetActive(false);
                    panelPause.gameObject.SetActive(false);
                    break;
                case GameManager.State.PAUSE:
                    OnPause.Invoke();
                    panelGame.gameObject.SetActive(false);
                    panelGameOver.gameObject.SetActive(false);
                    panelPause.gameObject.SetActive(true);
                    break;
                case GameManager.State.GAME_OVER:
                    Cursor.visible = true;
                    OnGameOver.Invoke();
                    panelGame.gameObject.SetActive(false);
                    panelGameOver.gameObject.SetActive(true);
                    panelPause.gameObject.SetActive(false);
                    break;
            }
        }

        private void Destroy()
        {
            GameManager.Instance.OnGameStateChange -= Instance_OnGameStateChange;
        }

        public void Resume()
        {
            GameManager.Instance.SetPause();
        }

        public void Quit()
        {
            Time.timeScale = 1;
            SceneManager.Instance.LoadLevelIndex(0);
        }

        public void QuitGame()
        {
            Application.Quit();
        }
    }
}