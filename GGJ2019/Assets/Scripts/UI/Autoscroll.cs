using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Autoscroll : MonoBehaviour
{
    [SerializeField]
    public float MaxScroll = 235f;

    [SerializeField]
    public float Speed = 0.05f;

    private ScrollRect scrollComponent { get; set; }
    
    private Vector2 targetPosition { get; set; }

    private bool scroll { get; set; } = true;

    void Start()
    {
        scrollComponent = GetComponent<ScrollRect>();
        targetPosition = new Vector2(scrollComponent.normalizedPosition.x, scrollComponent.normalizedPosition.y - MaxScroll);
    }

    void Update()
    {
        if (scroll)
        {
            scrollComponent.verticalNormalizedPosition -= Time.deltaTime * Speed;
            
            if (scrollComponent.verticalNormalizedPosition <= 0)
            {
                StopScroll();
                scrollComponent.verticalNormalizedPosition = 0;
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
