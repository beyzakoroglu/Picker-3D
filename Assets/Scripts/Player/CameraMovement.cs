using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private static CameraMovement instance;


    private GameObject player;
    private Vector3 offset;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    
    void Start()
    {
        player = Player.Instance.gameObject;
        offset = transform.position - player.transform.position;
    }

    void Update()
    {
        transform.position = new Vector3(0, player.transform.position.y + offset.y, player.transform.position.z + offset.z);
        // 433 iki kamera oldugu icin sanirim hata var çöz
    }
}
