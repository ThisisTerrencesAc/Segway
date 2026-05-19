using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : MonoBehaviour

{

    [SerializeField] AudioMixer mixer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    public void OnMasterVolumeChange(float value)
    {
        mixer.SetFloat("MasterVolume", value);

    }
    public void OnBGMVolumeChange(float value)
    {
        mixer.SetFloat("BackgroundVolume", value);

    }
    public void OnSFXVolumeChange(float value)
    {
        mixer.SetFloat("SFXVolume", value);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
