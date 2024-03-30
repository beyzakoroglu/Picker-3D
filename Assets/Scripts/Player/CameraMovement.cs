using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cube;
    private Vector3 offset;
    
    void Start()
    {
        offset = transform.position - cube.transform.position;
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        transform.position = new Vector3(0, cube.transform.position.y + offset.y, cube.transform.position.z + offset.z);
    }
}
