using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private static PlayerMovementController instance;
    public static PlayerMovementController Instance { get { return instance; } }



    private PlayerInputController playerInputController;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSpeedLimit;
    private bool canMove = true;

    private Vector3 _leftlimit;
    private Vector3 _rightlimit;

    private Vector3 _leftWall;
    private Vector3 _rightWall;
    private Rigidbody rb;
    public float newPositionX;

/*
    private void Awake()    //singleton pattern
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
*/

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerInputController =  GetComponent<PlayerInputController>();



        _leftlimit = transform.Find("leftlimit").position;
        _rightlimit = transform.Find("rightlimit").position;
        _leftWall = GameObject.FindWithTag("LeftWall").transform.position;
        _rightWall = GameObject.FindWithTag("RightWall").transform.position;
        
    }

    void Update()
    {
        if (canMove) {
            SetPlayerHorizontalMovement();
        }
    }

    private void SetPlayerHorizontalMovement() {
        float horizontalValue = playerInputController.HorizontalValue;

        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, forwardSpeed);

        if (horizontalValue < 0 && _leftlimit.x < _leftWall.x)
        {
            transform.position = new Vector3(transform.position.x + (_leftWall.x -_leftlimit.x), transform.position.y, transform.position.z);
            return;   //if we are so left, we can't move anymore
        }
        else if (horizontalValue > 0 && _rightlimit.x > _rightWall.x)
        {
            transform.position = new Vector3(transform.position.x + (_rightWall.x - _rightlimit.x), transform.position.y, transform.position.z);
            return; //if we are so right, we can't move anymore
        }
        rb.velocity = new Vector3(horizontalValue * horizontalSpeed, rb.velocity.y, forwardSpeed);
        
    }


    public void SetCanMove(bool value) {
        Debug.Log("SetCanMove" + value);
        canMove = value;

        if (!canMove) {
            rb.velocity = Vector3.zero;
        }
    }


}
