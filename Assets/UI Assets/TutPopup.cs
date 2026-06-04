using UnityEngine;

public class TutPopup : MonoBehaviour
{
    [SerializeField] private GameObject popupPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenPupup()
    {
        popupPanel.SetActive(true);
    }
}
