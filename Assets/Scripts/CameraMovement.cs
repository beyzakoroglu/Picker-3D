using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject cube;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - cube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, cube.transform.position.y + offset.y, cube.transform.position.z + offset.z);
    }
}
