using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOptions : MonoBehaviour
{
    [SerializeField] private SoundManager soundManager;
    private AudioMixer mixer;
    [SerializeField] private Slider general;
    [SerializeField] private Slider music;
    [SerializeField] private Slider effect;

    private void OnEnable()
    {
        soundManager = GameObject.Find("SoundManager").GetComponent<SoundManager>();
        mixer = soundManager.mixer;
    }

    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        SetVolume("MasterVolume", general.value);
        SetVolume("MusicVolume", music.value);
        SetVolume("EffectsVolume", effect.value);
    }

    private void SetVolume(string mixerVolume, float sliderValue)
    {
        if (sliderValue != 0)
        {
            mixer.SetFloat(mixerVolume, Mathf.Log10(sliderValue) * 20);
        }
        else
        {
            mixer.SetFloat(mixerVolume, Mathf.Log10(0.001f) * 20);
        }

    }
}
