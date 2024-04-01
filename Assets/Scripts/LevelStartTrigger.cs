using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = LevelManager.Instance;
            levelManager.UnloadPreviousLevel();
        }
    }
}
