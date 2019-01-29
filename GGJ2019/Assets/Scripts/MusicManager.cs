using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource Game;
    public AudioSource Score;

    private bool _resumed;

    public void PlayGame()
    {
        Game.Play();
        Score.Stop();
    }

    public void PlayScore()
    {
        Game.Stop();
        Score.Play();
    }

    public void ResumeGame()
    {
        _resumed = !_resumed;
        if (!_resumed)
            Game.pitch = 0.90f;
        else
            Game.pitch = 1;
    }
}
