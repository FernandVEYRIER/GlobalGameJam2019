using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public Slider master;
    public Slider music;
    public Slider effects;

    public AudioMixer mixer;
    public UnityEvent openEvent;
    public UnityEvent closeEvent;

    private bool _muted;

    private float _masterSound;
    private float _musicSound;
    private float _effectsSound;

    private float previousMaster;
    private bool isOpen;

    void Start()
    {
        SetMaster(master.value);
        SetMusic(music.value);
        SetEffects(effects.value);
    }

    public void SetMaster(float value)
    {
        mixer.SetFloat("MasterVolume", value);
    }

    public void SetMusic(float value)
    {
        mixer.SetFloat("MusicVolume", value);
    }

    public void SetEffects(float value)
    {
        mixer.SetFloat("EffectsVolume", value);
    }

    public void SetMuted(bool value)
    {
        if (value)
            previousMaster = master.value;
        mixer.SetFloat("MasterVolume", value ?  -80f : previousMaster);
    }

    public void Open()
    {
        var bg = GetComponent<Image>();
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0.9f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        openEvent.Invoke();
        isOpen = true;
    }

    public void Close()
    {
        var bg = GetComponent<Image>();
        bg.color = new Color(bg.color.r, bg.color.g, bg.color.b, 0f);
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        closeEvent.Invoke();
        isOpen = false;
    }

    void Update()
    {
        if (Input.GetButtonDown("SoundSettings"))
        {
            if (isOpen)
                Close();
            else
                Open();
        }
    }
}
