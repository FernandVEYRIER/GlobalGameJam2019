using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadPreviousLevel()
    {
        SceneManager.Instance.LoadPreviousLevel();
    }

    public void LoadNextLevel()
    {
        SceneManager.Instance.LoadNextLevel();
    }

    public void LoadMainMenu()
    {
        SceneManager.Instance.LoadLevelIndex(0);

    }
}
