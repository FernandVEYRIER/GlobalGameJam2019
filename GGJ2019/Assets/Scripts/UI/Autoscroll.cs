using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autoscroll : MonoBehaviour
{
    [SerializeField]
    public float Speed = 0.5f;

    private ScrollRect scrollComponent { get; set; }

    private Vector2 targetPosition { get; set; }

    public bool scroll { get; set; } = true;

    void Start()
    {
        scrollComponent = GetComponent<ScrollRect>();
        targetPosition = new Vector2(scrollComponent.normalizedPosition.x, scrollComponent.normalizedPosition.y + scrollComponent.preferredHeight);
    }

    void Update()
    {
        if (scroll)
        {
            scrollComponent.normalizedPosition = Vector2.Lerp(scrollComponent.normalizedPosition, targetPosition, Speed * Time.deltaTime);

            if (scrollComponent.normalizedPosition == targetPosition)
            {
                StopScroll();
            }
        }
    }

    public void StartScroll()
    {
        scroll = true;
    }

    public void StopScroll()
    {
        scroll = false;
    }
}
