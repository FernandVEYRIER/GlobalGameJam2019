using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float scaleFactor = 0.2f;
    public float floatingValue = 0.1f;
    public float pulsationSpeed = 10f;
    public float floatingSpeed = 1f;

    private float currentFactor;

    private Vector3 initialPos;
    private Vector3 initialScale;

    private Vector3 maxPos;

    private Vector3 minPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        initialScale = transform.localScale;
        maxPos = transform.position + Vector3.up * floatingValue;
        minPos = transform.position + Vector3.up * (-floatingValue);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = (Mathf.PingPong(Time.time * pulsationSpeed, 1f) * scaleFactor) * Vector3.one + initialScale;
        transform.position = initialPos + Vector3.up * (Mathf.PingPong(Time.time * floatingSpeed, 1f) * floatingValue);
    }
}
