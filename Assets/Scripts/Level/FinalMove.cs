using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalMove : MonoBehaviour
{
    LevelManager levelManager;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        levelManager = LevelManager.Instance;
        float offset = levelManager.GetElementRatio() * Constants.FINAL_SLIDE_SIZE;
        PlayerMovementController playerMovementController = Player.Instance.PlayerMovementController;
        playerMovementController.SetCanMove(false);

        StartCoroutine(LerpPosition(new Vector3(playerMovementController.transform.position.x, playerMovementController.transform.position.y, transform.position.z + offset)));
    
        yield return new WaitForSeconds(Constants.WIN_SLIDE_DURATION);

        Celebrate();
    
    }



    private IEnumerator LerpPosition(Vector3 targetPosition)
    {
        float time = 0;
        float duration = Constants.WIN_SLIDE_DURATION;

        PlayerInputController playerInputController = Player.Instance.PlayerInputController;
        Vector3 startPosition = playerInputController.transform.position;

        while (time < duration)
        {
            float t = time / duration;
            t = t * t * (3f - 2f * t); // Smooth step calculation for easing
            playerInputController.transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            time += Time.deltaTime;
            levelManager.SetElementView(Mathf.Lerp(levelManager.GetElementRatio(), 0, t)); // Update the slider value

            yield return null;
        }

        playerInputController.transform.position = targetPosition; // Ensure the final position is set correctly
        levelManager.ResetElementCount(); // Reset the element count for the next level
    }


    private void Celebrate()
    {
       GameManager.Instance.LevelWon();
    }


}
