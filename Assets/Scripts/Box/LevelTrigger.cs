using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            int curLevel = FindObjectOfType<LevelManager>().CurrentLevel;
            Debug.Log("Level " + curLevel + "Completed");

            other.gameObject.GetComponent<PlayerMovementController>().SetCanMove(false);
            other.gameObject.GetComponent<Player>().ThrowObjects();

            FindObjectOfType<LevelManager>().LoadNextLevel();
            
            if(curLevel >= 1)
            {
                FindObjectOfType<LevelManager>().UnloadPreviousLevel();
            }
            
        }
    }
    
}
