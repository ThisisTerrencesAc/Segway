using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParam;
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetMusicVolume()
    {
            float volumeinDb = Mathf.Log10(Mathf.Max(audioSlider.value, 0.00001f)) * 20f;
            mixer.SetFloat(exposedMusicParam, volumeinDb);
    }
    public void SetSFXVolume()
    {
            float volumeinDb = Mathf.Log10(Mathf.Max(sfxSlider.value, 0.00001f)) * 20f;
            mixer.SetFloat("SFXVolume", volumeinDb);
    }
    public void SetBGVolume()
    {
            float volumeinDb = Mathf.Log10(Mathf.Max(bgSlider.value, 0.00001f)) * 20f;
            mixer.SetFloat("BackgroundVolume", volumeinDb);

    }


}
