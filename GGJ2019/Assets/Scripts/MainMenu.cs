using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityTools.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject PlayerButtons;
    public GameObject Credits;
    public GameObject Exit;
    public GameObject Sound;
    public Animator P1Button;
    public Animator P2Button;
    public AudioSource P1;
    public AudioSource P2;
    public GameObject CreditsPanel;
    private string[] buildInputs = new[] { "BuildP1", "BuildP2" };
    private string[] throwInputs = new[] { "ThrowP1", "ThrowP2" };
    private bool p1Ready = false;
    private bool p2Ready = false;

    private bool isLoding = false;

    private bool visible = true;
    // Start is called before the first frame update

    // Update is called once per frame

    void Start()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        if (visible)
        {
            CheckP1();
            CheckP2();
        }
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
        CreditsPanel.SetActive(true);
        Hide();
    }

    public void CloseCredits()
    {
        CreditsPanel.SetActive(false);
        Show();
        SelectCredits();
    }

    public void SelectCredits()
    {
        Credits.GetComponent<Button>().Select();
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Show()
    {
        PlayerButtons.SetActive(true);
        Credits.SetActive(true);
        Exit.SetActive(true);
        Sound.SetActive(true);
        visible = true;
    }

    public void Hide()
    {
        PlayerButtons.SetActive(false);
        Credits.SetActive(false);
        Exit.SetActive(false);
        Sound.SetActive(false);
        visible = false;
    }
}
