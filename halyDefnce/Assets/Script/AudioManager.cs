using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;


[System.Serializable]
public class Sands
{
    public string soundName;
    public AudioClip audioClip;
    public AudioMixerGroup mixer;
    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float spatialBlend;


    public bool loop;


    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{


    public Sands[] sounds;

    private void Awake()
    {
        foreach (Sands s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.audioClip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
            s.source.outputAudioMixerGroup = s.mixer;
        }
    }
    private void Start()
    {
        Play("Bakgrundsund");
    }
    public void Play(string name)
    {
        Sands s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.Log("Sound:  " + name + "not found!!!");
            return;
        }
        s.source.Play();
    }
    public void StopPlay(string name)
    {
        Sands s = Array.Find(sounds, sound => sound.soundName == name);
        if (s == null)
        {
            Debug.Log("Sound:  " + name + "not found!!!");
            return;
        }
        s.source.Stop();
    }
}
