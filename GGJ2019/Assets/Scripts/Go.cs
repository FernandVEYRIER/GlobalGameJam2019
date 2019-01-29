using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Go : MonoBehaviour
{
    public AudioSource sourceP1;
    public AudioSource sourceP2;
    public void OnEnable()
    {
        sourceP1.Play();
        sourceP2.Play();
    }
}
