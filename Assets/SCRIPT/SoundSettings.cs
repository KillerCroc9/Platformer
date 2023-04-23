using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    AudioSource[] audioSources;
    public AudioSource Music;

    // Start is called before the first frame update
    void Start()
    {
        musicSlider.value = PlayerPrefs.GetFloat("Music", 1);
        sfxSlider.value = PlayerPrefs.GetFloat("SFX", 1);
        audioSources = FindObjectsOfType<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("Music", musicSlider.value);
        PlayerPrefs.SetFloat("SFX", sfxSlider.value);
        SoundChecker();
    }
    void SoundChecker()
    {
        foreach (AudioSource source in audioSources)
        {
            // Apply settings to all audio sources except the one named "AudioToExclude"
            source.volume = PlayerPrefs.GetFloat("SFX");
        }
        Music.volume = PlayerPrefs.GetFloat("Music");
    }
}
