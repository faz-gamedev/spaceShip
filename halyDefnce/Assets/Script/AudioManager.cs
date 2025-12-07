using RTLTMPro;
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

    [SerializeField] private string[] backGrandSund;
    [SerializeField] private string[] musicName;
    [SerializeField] private RTLTextMeshPro musicText;
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

        ChengMuseic(PlayerPrefs.GetInt("BackgrundMusic", 0));
    }

    public void ChengMuseic(int index)
    {
        if (musicText != null)
        {

            musicText.text = musicName[index];
        }
        Play(backGrandSund[index]);
    }
    public void ChengMuseikNext()
    {
        StopPlay(backGrandSund[PlayerPrefs.GetInt("BackgrundMusic")]);
        PlayerPrefs.SetInt("BackgrundMusic", PlayerPrefs.GetInt("BackgrundMusic", 0) + 1);
        if (PlayerPrefs.GetInt("BackgrundMusic") > backGrandSund.Length - 1)
        {
            PlayerPrefs.SetInt("BackgrundMusic", 0);
        }
        ChengMuseic(PlayerPrefs.GetInt("BackgrundMusic", 0));
        Debug.Log("play music  :" + PlayerPrefs.GetInt("BackgrundMusic", 0));
    }
    public void ChengMuseikBack()
    {
        StopPlay(backGrandSund[PlayerPrefs.GetInt("BackgrundMusic")]);
        PlayerPrefs.SetInt("BackgrundMusic", PlayerPrefs.GetInt("BackgrundMusic", 0) - 1);
        if (PlayerPrefs.GetInt("BackgrundMusic") < 0)
        {
            PlayerPrefs.SetInt("BackgrundMusic", backGrandSund.Length - 1);
        }
        ChengMuseic(PlayerPrefs.GetInt("BackgrundMusic", 0));
        Debug.Log("play music  :" + PlayerPrefs.GetInt("BackgrundMusic", 0));
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
