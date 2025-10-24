using System.Numerics;
using Rewired;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

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
    bool _rockPowerActivated;
    private float previousMoveAxis;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
        _rwPlayer = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        ManageMove();
        ManageRockPower();
    }

    void ManageMove()
    {
        float moveAxis = _rwPlayer.GetAxis("Move");
        float direction = moveAxis == 0 ? previousMoveAxis : moveAxis;
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position - _collider.bounds.size / 2, Vector2.down, 0.2f, LayerMask.GetMask("Ground", "Rock"));
        RaycastHit2D hitRock = Physics2D.Raycast(transform.position, Vector2.right * direction, 0.6f, LayerMask.GetMask("Rock"));
        Debug.DrawRay(transform.position, Vector2.right * direction * 0.2f, Color.red);
        
        _isGrounded = hitGround.collider;
        _isTouchingRock = hitRock.collider;

        if (_isTouchingRock && !_rockPowerActivated)
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody.velocity = new Vector2(0, _rwPlayer.GetAxis("Climb") * climbSpeed);
        }
        else
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
            _rigidBody.velocity = new Vector2(_rwPlayer.GetAxis("Move") * speed, _rigidBody.velocity.y);
        }
        
        if (_isGrounded && _rwPlayer.GetButtonDown("Jump"))
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (moveAxis != 0)
        {
            previousMoveAxis = moveAxis;
        }

    }

    void ManageRockPower()
    {
        if (_rwPlayer.GetButtonDown("RockPower"))
        {
            _rockPowerActivated = !_rockPowerActivated;
            var rocks = FindObjectsByType<Rock>(FindObjectsSortMode.None);
            foreach (Rock rock in rocks)
            {
                rock.ToggleState();
            }
        }
    }
}
