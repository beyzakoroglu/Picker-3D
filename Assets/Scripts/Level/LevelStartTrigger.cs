using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartTrigger : MonoBehaviour
{
    LevelManager levelManager;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Level Start Triggered");
            levelManager = LevelManager.Instance;
            levelManager.UnloadPreviousLevel();
            levelManager.LoadNextLevel();
            levelManager.ElementGoal = GameObject.FindGameObjectsWithTag("Element").Length;
        }
    }

}
