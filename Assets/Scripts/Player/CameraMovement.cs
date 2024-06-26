using UnityEngine;

public class CameraMovement : MonoBehaviour
{


    private GameObject player;
    private Vector3 offset;


    void Start()
    {
        player = Player.Instance.gameObject;
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(0, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
    }
}
