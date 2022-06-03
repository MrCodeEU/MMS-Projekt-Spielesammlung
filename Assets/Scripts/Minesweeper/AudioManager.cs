using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public AudioMixerGroup audioMixer;
    /// <summary>
    /// Gets all audio files
    /// </summary>
    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }
    /// <summary>
    /// Playes the audio file specifed by name
    /// </summary>
    /// <param name="name">The name of the sound effect</param>
    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        s.source.Play();
    }
}
