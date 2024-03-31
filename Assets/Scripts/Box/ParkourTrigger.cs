using UnityEngine;

public class ParkourTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = LevelManager.Instance;
            PlayerMovementController.Instance.SetCanMove(false);


            if(!Player.Instance.HasElements())
            {
                GameManager.Instance.ActivateLoseUI();
            } 
            else {
                Debug.Log("You Win the level: " + levelManager.CurrentLevel + "parkour: " + levelManager.CurrentParkour);
                other.gameObject.GetComponent<Player>().ThrowObjects();
            }

            
        }
    }
    
}
