using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Level Completed");
            other.gameObject.GetComponent<PlayerMovementController>().SetCanMove(false);
            other.gameObject.GetComponent<Player>().ThrowObjects();
        }
    }
    
}
