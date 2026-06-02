using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    
    public static float audioSliderVal =1.0f;
    public static float sfxSliderVal = 1.0f;
    public static float bgSliderVal = 1.0f;
    [SerializeField] AudioMixer mixer;
    [SerializeField] string exposedMusicParam;
    [SerializeField] Slider audioSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider bgSlider;
    [SerializeField] private AudioSource sfxSource;
    public static AudioManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSlider.value = audioSliderVal;
        sfxSlider.value = sfxSliderVal;
        bgSlider.value = bgSliderVal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void Awake()
    {
        if(instance == null)
        {
                instance = this;
                DontDestroyOnLoad(gameObject);
        }


        
    }
    public void SetMusicVolume()
    {
            audioSliderVal = audioSlider.value;
            float volumeinDb = Mathf.Log10(Mathf.Max(audioSliderVal, 0.00001f)) * 20f;
            mixer.SetFloat(exposedMusicParam, volumeinDb);
    }
    public void SetSFXVolume()
    {
            sfxSliderVal = sfxSlider.value;
            float volumeinDb = Mathf.Log10(Mathf.Max(sfxSliderVal, 0.00001f)) * 20f;
            mixer.SetFloat("SFXVolume", volumeinDb);
    }
    public void SetBGVolume()
    {
            bgSliderVal = bgSlider.value;
            float volumeinDb = Mathf.Log10(Mathf.Max(bgSliderVal, 0.00001f)) * 20f;
            mixer.SetFloat("BackgroundVolume", volumeinDb);

    }
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }


}
