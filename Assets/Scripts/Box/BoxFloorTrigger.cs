using System.Collections.Generic;
using UnityEngine;

public class BoxFloorTrigger : MonoBehaviour
{

    private List<Element> objectsInsideBox = new List<Element>();
    public Box box;
    
    //counts the balls inside the box and expledes them
    void OnTriggerEnter(Collider other)
    {
        Element element = other.gameObject.GetComponent<Element>();
        
        if(element==null){
            box.incrementScoreCount(0);
            return;
        }

        else if (!objectsInsideBox.Contains(element) && other.gameObject.CompareTag(Constants.Tags.BALL_TAG))
        {
            objectsInsideBox.Add(element);
            box.incrementScoreCount(1);
            element.ExplodeInTime();
        }
    }



}
