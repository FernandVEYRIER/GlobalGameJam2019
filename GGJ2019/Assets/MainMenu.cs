using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    public Animator P1Button;
    public Animator P2Button;

    private string[] buildInputs = new[] { "BuildP1", "BuildP2" };
    private string[] throwInputs = new[] { "ThrowP1", "ThrowP2" };


    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        CheckP1();
        CheckP2();
    }

    void CheckP1()
    {
        if (Input.GetButtonDown(buildInputs[0]))
        {
            P1Button.SetTrigger("Ready");
        }
    }

    void CheckP2()
    {
        if (Input.GetButtonDown(buildInputs[1]))
        {
            P2Button.SetTrigger("Ready");
        }
    }
}
