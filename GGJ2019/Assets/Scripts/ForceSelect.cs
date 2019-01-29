using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ForceSelect : MonoBehaviour
{
    void Start()
    {
        GetComponent<Selectable>().Select();
    }
    void OnEnable()
    {
        GetComponent<Selectable>().Select();
    }
}
