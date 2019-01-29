using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Game;
using UnityEngine;

public class DestroyEffect : MonoBehaviour
{
    public Color colorMin;
    public Color colorMax;
    public GameObject DestroyFx;
    private Vector3 lastVelocity;
    private Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void OnDestroy()
    {
        if (!_rb.isKinematic)
        {
            
            var obj = Instantiate(DestroyFx, transform.position, Quaternion.LookRotation(lastVelocity ,Vector3.up));
            var particules = obj.GetComponentsInChildren<ParticleSystem>();
            for (int i = 0; i < particules.Length; i++)
            {
                var mainModule = particules[i].main;
                var mainModuleStartColor = mainModule.startColor;
                mainModuleStartColor.colorMin = colorMin;
                mainModuleStartColor.colorMax = colorMax;
                mainModule.startColor = mainModuleStartColor;
                GameManager.Instance.CamShake();
            }
           
        }
    }

    void Update()
    {
        lastVelocity = -_rb.velocity;
    }
}
