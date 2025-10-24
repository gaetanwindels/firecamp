using Rewired;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Config parameters
    [SerializeField] private float speed = 10f;
    [SerializeField] private float climbSpeed = 5f;
    
    [SerializeField] private float jumpForce = 10f;
    
    // Cached variables
    private Rigidbody2D _rigidBody;

    private Collider2D _collider;

    private Player _rwPlayer;
    
    // State variable
    bool _isGrounded;
    bool _isTouchingRock;
    
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _rwPlayer = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        float moveAxis = _rwPlayer.GetAxis("Move");
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position - _collider.bounds.size / 2, Vector2.down, 0.1f, LayerMask.GetMask("Ground"));
        RaycastHit2D hitRock = Physics2D.Raycast(transform.position - _collider.bounds.size / 2, new Vector2(moveAxis, 0), 0.1f, LayerMask.GetMask("Rock"));
        _isGrounded = hitGround.collider;
        _isTouchingRock = hitGround.collider;

        if (_isTouchingRock)
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody.velocity = new Vector2(_rwPlayer.GetAxis("Move") * speed, _rigidBody.velocity.y);
        }

        if (_isGrounded && _rwPlayer.GetButtonDown("Climb"))
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x, _rwPlayer.GetAxis("Climb") * climbSpeed);
        }
        
        if (_isGrounded && _rwPlayer.GetButtonDown("Jump"))
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
