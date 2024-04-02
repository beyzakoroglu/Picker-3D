using System;
using System.Collections;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    private PlayerInputController playerInputController;
    [SerializeField] private float forwardSpeed;
    public float ForwardSpeed { get { return forwardSpeed; } set { forwardSpeed = value; } } 
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSpeedLimit;
    private bool canMove = false;

    private Transform _leftlimit;
    private Transform _rightlimit;

    private Vector3 _leftWall;
    private Vector3 _rightWall;
    private Rigidbody rb;



    public void Initialize()
    {
        rb = GetComponent<Rigidbody>();
        canMove = true;
        playerInputController =  GetComponent<PlayerInputController>();
        SetCanMove(true);

        _leftlimit = transform.Find("leftlimit");
        _rightlimit = transform.Find("rightlimit");
        _leftWall = GameObject.FindWithTag("LeftWall").transform.position; // stays the same
        _rightWall = GameObject.FindWithTag("RightWall").transform.position; // stays the same
    }

    void Update()
    {
        if (canMove) {
            SetPlayerHorizontalMovement();
        }
    }

    private void SetPlayerHorizontalMovement() {
        float horizontalValue = playerInputController.HorizontalValue;

        rb.velocity = new Vector3(rb.velocity.x, 0, forwardSpeed);
        if (horizontalValue < 0 && _leftlimit.position.x < _leftWall.x)
        {
            transform.position = new Vector3(transform.position.x + (_leftWall.x -_leftlimit.position.x), transform.position.y, transform.position.z);
            return;   //if we are so left, we can't move anymore
        }
        else if (horizontalValue > 0 && _rightlimit.position.x > _rightWall.x)
        {
            transform.position = new Vector3(transform.position.x + (_rightWall.x - _rightlimit.position.x), transform.position.y, transform.position.z);
            return; //if we are so right, we can't move anymore
        }
        rb.velocity = new Vector3(horizontalValue * horizontalSpeed, 0, forwardSpeed);
        
    }

    public void SetCanMove(bool value) {
        canMove = value;
        rb = GetComponent<Rigidbody>();
        if (!canMove) {
            rb.velocity = Vector3.zero;
        }
    }


}
