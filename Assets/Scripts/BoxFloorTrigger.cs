using System.Collections.Generic;
using UnityEngine;

public class BoxFloorTrigger : MonoBehaviour
{

     private List<GameObject> objectsInsideBox = new List<GameObject>();
    public Box box;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (!objectsInsideBox.Contains(other.gameObject) && other.gameObject.CompareTag("Ball"))
        {
            objectsInsideBox.Add(other.gameObject);
            box.incrementScoreCount();
            Debug.Log(box.getScoreCount() + " ball has fallen off the floor!");
        }
    }
}
