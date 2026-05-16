using UnityEngine;

public class camerafollow : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Transform player;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position;
    }
}
