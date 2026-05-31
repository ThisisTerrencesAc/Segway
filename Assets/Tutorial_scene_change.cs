using UnityEngine;
using UnityEngine.SceneManagement;

public class Tutorial_scene_change : MonoBehaviour
{
    public int sceneBuildIndex;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("tutorial_player"))
        {
            print("Switch scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex);
        }
    }
}