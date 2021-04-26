using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;

public class ParametersScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider MusicSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;
    void Start()
    {
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.50f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicLevel (float sliderVal)
	{
        audioMixer.SetFloat("MusicVol",Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("MusicVolume", sliderVal);
    }
    public void SetMainLevel(float sliderVal)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("MasterVolume", sliderVal);
    }
    public void SetSFXLevel(float sliderVal)
    {
        audioMixer.SetFloat("SFXVol", Mathf.Log10(sliderVal) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sliderVal);
    }
	public void ResetParameter()
	{
        audioMixer.SetFloat("MusicVol", Mathf.Log10(0.50f) * 20);
        audioMixer.SetFloat("MasterVol", Mathf.Log10(0.50f) * 20);
        audioMixer.SetFloat("SFXVol", Mathf.Log10(0.50f) * 20);
        PlayerPrefs.SetFloat("MusicVolume", 0.75f);
        PlayerPrefs.SetFloat("MasterVolume", 0.50f);
        PlayerPrefs.SetFloat("SFXVolume", 0.75f);
        MusicSlider.value = PlayerPrefs.GetFloat("MusicVolume", 0.75f);
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume", 0.75f);
        MasterSlider.value = PlayerPrefs.GetFloat("MasterVolume", 0.50f);
    }
}
