using System.Collections.Generic;
using UnityEngine;

public class BoxFloorTrigger : MonoBehaviour
{

    private List<Element> objectsInsideBox = new List<Element>();
    public Box box;

    void OnTriggerEnter(Collider other)
    {
        Element element = other.gameObject.GetComponent<Element>();
        
        if(element==null){return;}

        if (!objectsInsideBox.Contains(element) && other.gameObject.CompareTag(Constants.Tags.BALL_TAG))
        {
            objectsInsideBox.Add(element);
            box.incrementScoreCount();
            element.ExplodeInTime();
        }
    }



}
