using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

[RequireComponent(typeof(TextMeshProUGUI))]
public class Timer : MonoBehaviour
{
    public float time = 60f;
    public UnityEvent OnTimerFinished;

    private TextMeshProUGUI _timerText;

    void Start()
    {
        _timerText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            var finished = false;
            if (time <= 0f)
            {
                time = 0f;
                finished = true;
            }
            int minutes = Mathf.FloorToInt(time / 60F);
            int seconds = Mathf.FloorToInt(time - minutes * 60);
            _timerText.text = $"{minutes:0}:{seconds:00}";
            if (finished)
                OnTimerFinished.Invoke();
        }
    }
}
