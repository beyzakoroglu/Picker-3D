using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSpeedLimit;
    private bool canMove = true;

    private Transform _leftlimit;
    private Transform _rightlimit;

    private Transform _leftWall;
    private Transform _rightWall;
    private Rigidbody rb;
    public float newPositionX;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        _leftlimit = transform.Find("leftlimit");    // searching for the left limit object inside only the player object
        _rightlimit = transform.Find("rightlimit");
        _leftWall = GameObject.FindWithTag("LeftWall").transform;
        _rightWall = GameObject.FindWithTag("RightWall").transform;
        
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

        if (horizontalValue < 0 && _leftlimit.position.x < _leftWall.position.x)
        {
            transform.position = new Vector3(transform.position.x + (_leftWall.position.x-_leftlimit.position.x), transform.position.y, transform.position.z);
            return;
        }
        else if (horizontalValue > 0 && _rightlimit.position.x > _rightWall.position.x)
        {
            transform.position = new Vector3(transform.position.x + (_rightWall.position.x - _rightlimit.position.x), transform.position.y, transform.position.z);
            return;
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
