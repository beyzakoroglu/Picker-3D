using System.Collections.Generic;
using UnityEngine;

public class BoxFloorTrigger : MonoBehaviour
{

    private List<Element> objectsInsideBox = new List<Element>();
    public Box box;
    
    //counts the elements inside the box and expledes them
    void OnTriggerEnter(Collider other)
    {
        Element element = other.gameObject.GetComponent<Element>();
        
        if(element==null){
            return;
        }

        else if (!objectsInsideBox.Contains(element) && other.gameObject.CompareTag(Constants.Tags.ELEMENT_TAG))
        {
            objectsInsideBox.Add(element);
            box.IncrementScoreCount();
            element.ExplodeInTime();
        }
    }



}
