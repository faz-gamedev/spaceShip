using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SundSeting : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderVfx;
    public GameObject muteImg;
    public GameObject muteImg2;
  


    private bool isMuted = false;
    private void Start()
    {

        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            LoadVolum();
        }
        else
        {

            SetMusicVolume();
            SetSfxVolume();
        }
    }
    private void Update()
    {
        if (isMuted == true)
        {
            mixer.SetFloat("Musicw", -80);
            mixer.SetFloat("Sfxc", -80);
        }

    }
    public void SetMusicVolume()
    {
        float valum = sliderMusic.value;
        mixer.SetFloat("Musicw", valum);
        PlayerPrefs.SetFloat("MusicVolume", valum);
        if (sliderMusic.value <= -80)
        {
            muteImg.SetActive(true);
        }
        else
        {
            muteImg.SetActive(false);
        }

    
    }
    public void SetSfxVolume()
    {
        float valum2 = sliderVfx.value;
        mixer.SetFloat("sfxw", valum2);
        PlayerPrefs.SetFloat("SfxVolume", valum2);
        if (sliderVfx.value <= -80)
        {
            muteImg2.SetActive(true);
        }
        else
        {
            muteImg2.SetActive(false);
        }


     
    }

    private void LoadVolum()
    {
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume");
        sliderVfx.value = PlayerPrefs.GetFloat("SfxVolume");

        SetMusicVolume();
        SetSfxVolume();
    }
    public void Mute()
    {
        mixer.SetFloat("Musicw", -80);
        mixer.SetFloat("Sfxc", -80);

        isMuted = true;
    }
    public void UnMute()
    {
        isMuted = false;
        LoadVolum();

    }
}
