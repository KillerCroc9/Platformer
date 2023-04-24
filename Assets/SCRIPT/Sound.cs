using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public AudioSource[] audioSources;
    public AudioSource Music;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Delay", .5f); // Delay the sound check to allow the audio sources to be found
    }
    void Delay() 
    {
        audioSources = FindObjectsOfType<AudioSource>();
        Music = GetComponent<AudioSource>();
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
