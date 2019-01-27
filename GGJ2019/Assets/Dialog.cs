using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[Serializable]
public class Dialog
{
    public string name;
    [TextArea(3, 10)]
    public string[] sentences;
}
