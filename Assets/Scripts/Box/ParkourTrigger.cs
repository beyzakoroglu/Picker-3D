using UnityEngine;

public class ParkourTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player Arrived");
            LevelManager levelManager = LevelManager.Instance;
            Player.Instance.Stop();
            


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
