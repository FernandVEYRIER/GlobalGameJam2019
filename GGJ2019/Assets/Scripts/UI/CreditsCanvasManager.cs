using UnityEngine;
using UnityTools.SceneManagement;

namespace Assets.Scripts.UI
{
    public class CreditsCanvasManager : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            SceneManager.Instance.LoadLevelIndex(0);
        }
    }
}