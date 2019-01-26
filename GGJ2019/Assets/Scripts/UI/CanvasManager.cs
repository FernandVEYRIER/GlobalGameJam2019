using UnityEngine;

namespace Assets.Scripts.Game
{
    public class CanvasManager : MonoBehaviour
    {
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
                    panelGame.gameObject.SetActive(true);
                    panelGameOver.gameObject.SetActive(false);
                    panelPause.gameObject.SetActive(false);
                    break;
                case GameManager.State.PAUSE:
                    panelGame.gameObject.SetActive(false);
                    panelGameOver.gameObject.SetActive(false);
                    panelPause.gameObject.SetActive(true);
                    break;
                case GameManager.State.GAME_OVER:
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
    }
}