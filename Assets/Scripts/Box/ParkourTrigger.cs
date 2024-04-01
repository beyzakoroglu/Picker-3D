using UnityEngine;

public class ParkourTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LevelManager levelManager = LevelManager.Instance;
            Player.Instance.Stop();
            


            if(!Player.Instance.HasElements())
            {
                LevelManager.Instance.LoseLevel();
            } 
            else {
                other.gameObject.GetComponent<Player>().ThrowObjects();
            }

            
        }
    }
    
}
