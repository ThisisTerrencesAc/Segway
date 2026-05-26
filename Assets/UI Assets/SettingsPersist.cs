using UnityEngine;

public class SettingsPersist : MonoBehaviour
{
    public static SettingsPersist instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    void Awake()
    {
        
        if(instance == null)
        {
                instance = this;
                DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
