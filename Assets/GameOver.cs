using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOver : MonoBehaviour
{
    [SerializeField] private AudioClip sfxClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            AudioManager.instance.PlaySFX(sfxClip);
            SceneManager.LoadScene("GameOver");
        }
    }
}
