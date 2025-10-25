using System.Numerics;
using Rewired;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class PlayerController : MonoBehaviour
{
    // Config parameters
    [SerializeField] private float speed = 10f;
    [SerializeField] private float climbSpeed = 5f;
    
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float exitRockForce = 5f;
    
    // Cached variables
    private Rigidbody2D _rigidBody;

    private Animator _animator;
    
    private SpriteRenderer  _spriteRenderer;

    private Collider2D _collider;

    private Player _rwPlayer;
    
    // State variable
    bool _isGrounded;
    bool _isTouchingRock;
    bool _rockPowerActivated;
    private float _previousMoveAxis;
    private bool _previousTouchingRock;
    
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
        _rwPlayer = ReInput.players.GetPlayer(0);
    }

    void Update()
    {
        CheckTouching();
        ManageMove();
        ManageClimb();
        ManageJump();
        ManageAnimation();
        ManageRockPower();
    }

    void CheckTouching()
    {
        float moveAxis = _rwPlayer.GetAxis("Move");
        float direction = moveAxis == 0 ? _previousMoveAxis : moveAxis;

        _previousTouchingRock = _isTouchingRock;
        
        Vector2 originGrounded = new Vector2(transform.position.x, _collider.bounds.min.y);
        Vector2 originClimb = new Vector2(direction < 0 ? _collider.bounds.min.x : _collider.bounds.max.x, _collider.bounds.max.y - (_collider.bounds.size.y / 2));
        RaycastHit2D hitGround = Physics2D.Raycast(originGrounded, Vector2.down, 0.2f, LayerMask.GetMask("Ground", "Rock"));
        RaycastHit2D hitRock = Physics2D.Raycast(originClimb, Vector2.right * direction, 0.2f, LayerMask.GetMask("Rock"));  
        _isGrounded = hitGround.collider;
        _isTouchingRock = hitRock.collider;
  
        Debug.DrawRay(originClimb, Vector2.right * direction * 0.2f, Color.red);
    }

    void ManageMove()
    {
        float moveAxis = _rwPlayer.GetAxis("Move");
        
        if (_isGrounded)
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }
        
        _rigidBody.velocity = new Vector2(_rwPlayer.GetAxis("Move") * speed, _rigidBody.velocity.y);
        
        if (moveAxis != 0)
        {
            _previousMoveAxis = moveAxis;
        }

    }

    void ManageJump()
    {
        if (_isGrounded && _rwPlayer.GetButtonDown("Jump"))
        {
            _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void ManageClimb()
    {
        if (_isTouchingRock && !_rockPowerActivated && !_isGrounded)
        {
            _rigidBody.bodyType = RigidbodyType2D.Kinematic;
            _rigidBody.velocity = new Vector2(0, _rwPlayer.GetAxis("Climb") * climbSpeed);
        }

        if (!_isTouchingRock)
        {
            _rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }

        if (_previousTouchingRock && !_isTouchingRock)
        {
            _rigidBody.AddForce(Vector2.up * exitRockForce, ForceMode2D.Impulse);
        }
    }

    void ManageAnimation()
    {
        float moveAxis = _rwPlayer.GetAxis("Move");

        if (moveAxis != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(moveAxis), transform.localScale.y, transform.localScale.z);
        }
        
        _animator.SetBool("IsRunning", _rigidBody.velocity.x != 0);
        
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
