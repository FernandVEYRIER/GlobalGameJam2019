using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class bip : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private bool asBip;
    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
    }
    void Update()
    {
        if (tmp.color.a >= 0.99f && !asBip)
        {
            GetComponent<AudioSource>().Play();
            asBip = true;
        }
    }
}
