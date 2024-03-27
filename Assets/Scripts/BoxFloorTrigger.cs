using System.Collections.Generic;
using UnityEngine;

public class BoxFloorTrigger : MonoBehaviour
{

    private List<Element> objectsInsideBox = new List<Element>();
    public Box box;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        Element element = other.gameObject.GetComponent<Element>();
        
        if(element==null){return;}

        if (!objectsInsideBox.Contains(element) && other.gameObject.CompareTag("Ball"))
        {
            objectsInsideBox.Add(element);
            box.incrementScoreCount();
            element.ExplodeInTime();
        }
    }



}
