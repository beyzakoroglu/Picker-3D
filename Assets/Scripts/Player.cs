using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<GameObject> objectsInsideBasket = new List<GameObject>();



    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInsideBasket.Contains(other.gameObject) && other.gameObject.CompareTag("Ball"))
        {
            objectsInsideBasket.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInsideBasket.Contains(other.gameObject))
        {
            objectsInsideBasket.Remove(other.gameObject);
        }
    }



    public void ThrowObjects()
    {
        foreach (GameObject obj in objectsInsideBasket)
        {
            obj.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 40);
        }
        objectsInsideBasket.Clear();
    }



}
