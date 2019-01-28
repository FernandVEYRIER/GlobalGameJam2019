using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTools.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator P1Button;
    public Animator P2Button;
    public AudioSource P1;
    public AudioSource P2;

    private string[] buildInputs = new[] { "BuildP1", "BuildP2" };
    private string[] throwInputs = new[] { "ThrowP1", "ThrowP2" };
    private bool p1Ready = false;
    private bool p2Ready = false;

    private bool isLoding = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        CheckP1();
        CheckP2();
    }

    void CheckP1()
    {
        if (Input.GetButtonDown(buildInputs[0]) && isLoding == false)
        {
            P1Button.SetTrigger("Ready");
            p1Ready = !p1Ready;
            if (p1Ready)
                P1.Play();
            if (p1Ready && p2Ready)

            {
                isLoding = true;
                SceneManager.Instance.LoadNextLevel();
            }
        }
    }

    void CheckP2()
    {
        if (Input.GetButtonDown(buildInputs[1]) && isLoding == false)
        {
            P2Button.SetTrigger("Ready");
            p2Ready = !p2Ready;
            if (p2Ready)
                P2.Play();
            if (p1Ready && p2Ready)
            {
                isLoding = true;
                SceneManager.Instance.LoadNextLevel();
            }
        }
    }

    public void OpenCredits()
    {
        SceneManager.Instance.LoadLevelIndex(3);
    }
}
