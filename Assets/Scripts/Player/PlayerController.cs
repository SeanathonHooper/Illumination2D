using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;
    private bool _isGrounded;
    private const float MoveSpeed = 5f;
    private Vector2 _currentXVelocity = Vector2.zero;
    
    private Vector2 _playerInputMoveDir = new Vector2(0,0);
    
    private void Awake()
    {
        _isGrounded = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponent<BoxCollider2D>();
        _playerInput = new PlayerInput();
        
        //Setting up what input actions do
        _playerInput.Player.Move.performed += ctx => _playerInputMoveDir.x = ctx.ReadValue<float>();
        _playerInput.Player.Move.canceled += _ => _playerInputMoveDir.x = 0;

        _playerInput.Player.Jump.performed += _ => JumpPlayer();
        

    }

    private void OnEnable()
    {
        //Enables input, won't do anything otherwise
        _playerInput.Enable();
        Player player = GetComponent<Player>();
        player.OnPlayerDeath += () => _playerInput.Player.Disable();
        player.OnPlayerRespawn += () => _playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        //Disables input
        _playerInput.Disable();
    }

 
    private void MovePlayer()
    {
        if (_playerInputMoveDir.x != 0)
        {
            //Clamping the horizontal velocity only
            _currentXVelocity.x = _rigidbody.linearVelocity.x;
            _currentXVelocity = Vector2.ClampMagnitude(_currentXVelocity, 3);
            _currentXVelocity.y = _rigidbody.linearVelocity.y;
        
            _rigidbody.linearVelocity = _currentXVelocity;
        
            _rigidbody.AddForce(_playerInputMoveDir * MoveSpeed, ForceMode2D.Impulse);
        }
        else
        {
            _currentXVelocity = Vector2.zero;
            _currentXVelocity.y = _rigidbody.linearVelocity.y;
            _rigidbody.linearVelocity = _currentXVelocity;
        }
    }
    


    private void CheckIfGrounded()
    {
        //Does a raycast below the player to reset the jump counter
        if (Physics2D.Raycast(transform.position - new Vector3(_collider.bounds.size.x, 0, 0), transform.TransformDirection(Vector2.down), 0.7f, LayerMask.GetMask("Ground")) 
             || (Physics2D.Raycast(transform.position + new Vector3(_collider.bounds.size.x, 0, 0), transform.TransformDirection(Vector2.down), 0.7f, LayerMask.GetMask("Ground")) 
             || (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 0.7f, LayerMask.GetMask("Ground")))))
                                                           
        {
            _isGrounded = true;
        }

        else
        {
            _isGrounded = false;
        }
    }

    private void JumpPlayer()
    {
        if (_isGrounded)
        {
            // _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            float velocity = Mathf.Sqrt(2 * 9.81f * (transform.position.y + 7.5f));
            velocity -= _rigidbody.linearVelocity.y;
            _rigidbody.AddForce(Vector2.up * (velocity * _rigidbody.mass), ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }



    private void FixedUpdate()
    {
        MovePlayer();
        CheckIfGrounded();
    }
}
