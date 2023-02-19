using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RaycastHit2D))]
public class PhysicsMovement : MonoBehaviour
{
    private const float MinMoveDistance = 0.001f;
    private const float ShellRadius = 0.01f;

    public event Action<PlayerState> PlayerStateChanged;
    
    public LayerMask LayerMask;

    [SerializeField] private float _minGroundNormalY = .65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _speedMultiplier = 5f;
    [SerializeField] private float _jumpMultiplier = 3f;

    private Vector2 _velocity;
    private Vector2 _targetVelocity;

    private bool _isGrounded;
    private Vector2 _groundNormal;
    private Rigidbody2D _rb2d;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);    

    private PlayerState _previousPlayerState;
    private PlayerState _playerState;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(LayerMask);
        _contactFilter.useLayerMask = true;
    }

    public void SetTargetVelocity(Vector2 targetVelocity, bool isTryJump)
    {
        _targetVelocity = targetVelocity;

        if (isTryJump && _isGrounded)
        {
            _velocity = new Vector2(_velocity.x, 5);
        }
    }    

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.fixedDeltaTime;
        _velocity = new Vector2(_targetVelocity.x, _velocity.y);

        _isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.fixedDeltaTime ;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Vector2 deltaMovePosition = move * _speedMultiplier;

        Movement(deltaMovePosition, false);

        move = Vector2.up * deltaPosition.y;
        Vector2 deltaJumpPosition = move * _jumpMultiplier;

        Movement(deltaJumpPosition, true);

        SetPlayerState(deltaPosition);       
    }

    private void SetPlayerState(Vector2 deltaPosition)
    {
        _previousPlayerState = _playerState;

        if (deltaPosition.y > 0)
        {
            _playerState = PlayerState.Jump;
        }
        else if (Mathf.Abs(deltaPosition.x) > 0)
        {
            _playerState = PlayerState.Move;
        }
        else
        {
            _playerState = PlayerState.Idle;
        }

        if (_previousPlayerState != _playerState)
        {
            PlayerStateChanged?.Invoke(_playerState);
        }
    }

    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > MinMoveDistance)
        {
            int count = _rb2d.Cast(move, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;

                    if (yMovement)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rb2d.position = _rb2d.position + move.normalized * distance;
    }    
}