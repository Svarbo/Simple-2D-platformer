using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerMover : MonoBehaviour
{
    private const float ShellRadius = 0.01f;
    private const float _minMoveDistance = 0.001f;

    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private float _minGroundNormalY = 0.65f;
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpPower;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private Vector2 _targetVelocity;
    private Vector2 _velocity;
    private Vector2 _groundNormal;
    private ContactFilter2D _contactFilter;
    private RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    private List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);
    private bool _isGrounded;
    private float _directionIndicator;
    private int _isJumpAnimation = Animator.StringToHash("IsJump");
    private int _speedAnimationParameter = Animator.StringToHash("Speed");

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _directionIndicator = Input.GetAxis("Horizontal");
        _targetVelocity = new Vector2(_directionIndicator * _speed, 0);

        if (_directionIndicator < 0)
            _spriteRenderer.flipX = true;
        if(_directionIndicator > 0)
            _spriteRenderer.flipX = false;
        
        if (Input.GetKey(KeyCode.W) && _isGrounded)               
        {
            _velocity.y = _jumpPower;
        }

        _animator.SetInteger(_speedAnimationParameter, Convert.ToInt32(_directionIndicator * _speed));
        _animator.SetBool(_isJumpAnimation , _isGrounded != true);
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _isGrounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 targetMovePosition = moveAlongGround * deltaPosition.x;

        Move(targetMovePosition, false);

        targetMovePosition = Vector2.up * deltaPosition.y;

        Move(targetMovePosition, true);
    }

    private void Move(Vector2 targetMovePosition, bool isYMovement)
    {
        float distance = targetMovePosition.magnitude;

        if(distance > _minMoveDistance)
        {
            int count = _rigidbody2D.Cast(targetMovePosition, _contactFilter, _hitBuffer, distance + ShellRadius);

            _hitBufferList.Clear();

            for(int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for(int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;

                if (currentNormal.y > _minGroundNormalY)
                {
                    _isGrounded = true;

                    if (isYMovement)
                    {
                        _groundNormal = currentNormal;

                        currentNormal.x = 0;
                    }
                }
                
                float projection = Vector2.Dot(_velocity, currentNormal);

                if(projection < 0)
                {
                    _velocity -= projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - ShellRadius;

                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody2D.position += targetMovePosition.normalized * distance;
    }
}
