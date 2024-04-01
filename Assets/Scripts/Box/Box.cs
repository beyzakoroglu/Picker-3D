using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Box : MonoBehaviour
{
    [SerializeField] private Box box;    
    private PlayerMovementController playerMovementController;
    private LevelManager levelManager;

    // Score variables
    private int scoreCount = 0;
    private bool hasControlled = false; //to prevent multiple control calls
    [SerializeField] private int scoreToWin;   //constantslara koy her level için


    // Floor rising variables
    private Vector3 targetPosition; //target position of floor
    private float moveSpeed = 9f; //move speed of the floor 
    [SerializeField] private GameObject thefloor; //floor object


    // Barrier opening variables
    private GameObject rightBarrier;
    private GameObject leftBarrier;
    private Vector3 leftOpenRotation = new Vector3(0, 0, 60); //for left barrier rotation
    private Vector3 rightOpenRotation = new Vector3(0, 0, -60); //for left barrier rotation
    private float rotationSpeed = 5f; //rotation speed of the barriers

    public int GetScoreCount() => scoreCount;
    public int GetScoreToWin() => scoreToWin;

    void Start() {
        playerMovementController = FindObjectOfType<PlayerMovementController>();
        levelManager = LevelManager.Instance;

        rightBarrier = box.transform.Find("Gate/RightGate").gameObject;
        leftBarrier = box.transform.Find("Gate/LeftGate").gameObject;        
        targetPosition = new Vector3(thefloor.transform.position.x, transform.parent.position.y, thefloor.transform.position.z); //target position of the floor

    }

    public void IncrementScoreCount()
    {
        scoreCount++;
        if(!hasControlled){
            Invoke("Control", 2f);
            hasControlled = true;
        }
        levelManager.IncrementElementCount();
    }


    private void Control(){
        if(scoreCount >= scoreToWin)
        {
            WinParkour();
        }   
        else
        {
            LoseParkour();
        }
    }

    private void WinParkour(){
        Debug.Log("You Win the parkour!");
        StartCoroutine(EnterNextParkour());      
    }



    private void LoseParkour(){
        Debug.Log("You Lose!");
        levelManager.LoseLevel();
    }
    

    // rises the floor and opens the barriers
    private IEnumerator EnterNextParkour() 
    {
        StartCoroutine(RiseNewFloor());

        yield return new WaitForSeconds(1f);

        StartCoroutine(OpenBarriers());        
        GameManager.Instance.ActivateCongratsUI();

        yield return new WaitForSeconds(1.5f);
        
        playerMovementController.SetCanMove(true);
        GameManager.Instance.DeactivateCongratsUI();
        

    }

    private IEnumerator RiseNewFloor(){

        //smoothly move the floor to the target position
        while (Vector3.Distance(thefloor.transform.position, targetPosition) > 0.01f)
        {
            thefloor.transform.position = Vector3.Lerp(thefloor.transform.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null; 
        }
        thefloor.transform.position = targetPosition; //incase a small difference remains
    }


    private IEnumerator OpenBarriers(){

        DeactivePreventer();
        while (Quaternion.Angle(leftBarrier.transform.rotation, Quaternion.Euler(leftOpenRotation)) > 1.0f && Quaternion.Angle(rightBarrier.transform.rotation, Quaternion.Euler(rightOpenRotation)) > 1.0f)
        {
            leftBarrier.transform.rotation = Quaternion.Lerp(leftBarrier.transform.rotation, Quaternion.Euler(leftOpenRotation), rotationSpeed * Time.deltaTime);

            rightBarrier.transform.rotation = Quaternion.Lerp(rightBarrier.transform.rotation, Quaternion.Euler(rightOpenRotation), rotationSpeed * Time.deltaTime);

            yield return null;
        }

        // incase a small difference remains
        leftBarrier.transform.rotation = Quaternion.Euler(leftOpenRotation);
        rightBarrier.transform.rotation = Quaternion.Euler(rightOpenRotation);
    }

    private void DeactivePreventer(){
        //deactivate the preventers
        Transform preventer = transform.Find("LastPreventer");
        if(preventer != null){
            preventer.gameObject.SetActive(false);
        }
    }


}
