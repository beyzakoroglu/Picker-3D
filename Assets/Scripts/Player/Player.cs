using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<GameObject> objectsInsideMagnet = new List<GameObject>();

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInsideMagnet.Contains(other.gameObject) && other.gameObject.CompareTag("Ball"))
        {
            objectsInsideMagnet.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (objectsInsideMagnet.Contains(other.gameObject))
        {
            objectsInsideMagnet.Remove(other.gameObject);
        }
    }

    public void ThrowObjects()
    {
        if(objectsInsideMagnet.Count == 0)
        {   
            Debug.Log("No objects to throw");
            //return;
        }
        
        foreach (GameObject obj in objectsInsideMagnet)
        {
            obj.GetComponent<Element>().Throw();
        }
        objectsInsideMagnet.Clear();
    }

}
