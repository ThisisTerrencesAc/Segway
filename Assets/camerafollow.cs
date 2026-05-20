
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public Vector2 deadzoneX;
    public Vector2 deadzoneY;

    private Vector3 initSpeed = new Vector3(0, 1f, 0);
    private void Start()
    {
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
        deadzoneX = new Vector2(player.position.x - 10f, player.position.x + 10f);
        deadzoneY = new Vector2(player.position.y - 10f, player.position.y + 10f);
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, player.transform.position + new Vector3(0, 0, -10), ref initSpeed, 0.5f);
        //     Vector3 targetPos = player.position;
        //     Vector3 cameraPos = transform.position;

        //     Debug.Log(deadzoneX);
        //     Debug.Log(deadzoneY);

        //     if (targetPos.x < deadzoneX.x || targetPos.x > deadzoneX.y)
        //     {
        //         if (targetPos.x < deadzoneX.x)
        //         {
        //             cameraPos.x = targetPos.x - 10f;
        //         }
        //         else if (targetPos.x > deadzoneX.y)
        //         {
        //             cameraPos.x = targetPos.x + 10f;
        //         }
        //         deadzoneX = new Vector2(player.position.x - 20f, player.position.x + 20f);
        //     }

        //     if (targetPos.y < deadzoneY.x || targetPos.y > deadzoneY.y)
        //     {
        //         if (targetPos.y < deadzoneY.x)
        //         {
        //             cameraPos.y = targetPos.x - 10f;
        //         }
        //         else if (targetPos.y > deadzoneY.y)
        //         {
        //             cameraPos.y = targetPos.y + 10f;
        //         }
        //         deadzoneY = new Vector2(player.position.y - 20f, player.position.y + 10f);
        //     }

        //     transform.position = cameraPos;
        // }
    }
}