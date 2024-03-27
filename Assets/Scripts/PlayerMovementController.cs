using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private PlayerInputController playerInputController;
    [SerializeField] private float forwardSpeed;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float horizontalSpeedLimit;

    private Transform _leftlimit;
    private Transform _rightlimit;

    private Transform _leftWall;
    private Transform _rightWall;
    private Rigidbody rigidbody;
    public float newPositionX;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        _leftlimit = transform.Find("leftlimit");    // searching for the left limit object inside only the player object
        _rightlimit = transform.Find("rightlimit");
        _leftWall = GameObject.FindWithTag("LeftWall").transform;
        _rightWall = GameObject.FindWithTag("RightWall").transform;
        
    }

    void Update()
    {
        SetPlayerHorizontalMovement();
    }

    private void SetPlayerHorizontalMovement() {
        float horizontalValue = playerInputController.HorizontalValue;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, rigidbody.velocity.y, forwardSpeed);
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
        rigidbody.velocity = new Vector3(horizontalValue * horizontalSpeed, rigidbody.velocity.y, forwardSpeed);
        
    }
}
