using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded;
    private const float MoveSpeed = 5f;
    private const  float JumpForce = 15f;
    private Vector2 _currentXVelocity = Vector2.zero;
    
    private Vector2 _playerInputMoveDir = new Vector2(0,0);
    
    
    private void Awake()
    {
        _isGrounded = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = new PlayerInput();
        
        _playerInput.Player.Move.performed += ctx => _playerInputMoveDir.x = ctx.ReadValue<float>();
        _playerInput.Player.Move.canceled += _ => _playerInputMoveDir.x = 0;

        _playerInput.Player.Jump.performed += _ => JumpPlayer();

    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    // ReSharper disable Unity.PerformanceAnalysis
    private void MovePlayer()
    {
        if (_playerInputMoveDir.x != 0)
        {
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

    // ReSharper disable Unity.PerformanceAnalysis
    private void CheckIfGrounded()
    {
        if (Physics2D.Raycast(transform.position, transform.TransformDirection(Vector2.down), 1.2f, LayerMask.GetMask("Ground")) && (_rigidbody.linearVelocityY <= 0.001f))
        {
            _isGrounded = true;
            Debug.Log("Grounded");
        }
    }

    private void JumpPlayer()
    {
        if (_isGrounded)
        {
            _rigidbody.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
        CheckIfGrounded();
    }
}
