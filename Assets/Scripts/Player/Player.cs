using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private static Player player;
    public static Player Instance { get => player; }


    private List<GameObject> objectsInsideMagnet = new List<GameObject>();



    void Awake()
    {
        if (player != null)
        {
            Destroy(gameObject);
        }
        
        player = this;
        DontDestroyOnLoad(gameObject);
    
    }



    private void OnTriggerEnter(Collider other)
    {
        if (!objectsInsideMagnet.Contains(other.gameObject) && other.gameObject.CompareTag(Constants.Tags.ELEMENT_TAG))
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
        foreach (GameObject obj in objectsInsideMagnet)
        {
            obj.GetComponent<Element>().Throw();
        }
        objectsInsideMagnet.Clear();
    }

    public bool HasElements()
    {
        return objectsInsideMagnet.Count != 0;
    }

    public void RestartPlayer()
    {
        objectsInsideMagnet.Clear();
        //gameObject.transform.position = new Vector3(); // burayı grounda bağlı bişe yapcaz;
    }

}
