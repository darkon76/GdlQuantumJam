using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    SpriteRenderer _spriteRenderer;
    Animator _animator;
    Rigidbody2D _rigid;


    [Header("Movement")]
    public float Speed = 300f;
    private bool _lookingRight = true;
    public float _direction = 0;
    [Header("Jump")]
    public float JumpForce = 200f;
    private bool _wantToJump = false;
    private bool _isJumping = false;
    private float _jumpTimer = 0;
    [SerializeField]
    private float _jumpTime = 1;

    [Header("Ground Detection")]
    public bool IsGrounded = false;
    [SerializeField]
    float _radiousCheck;
    [SerializeField]
    LayerMask _goundMask;
    [Header("Ladder")]
    public bool AtLadder = false;
    [SerializeField]
    private string _ladderTag = "Ladder";
    private HashSet<Collider2D> _ladderConnections = new HashSet<Collider2D>();
    Collider2D[] _colliders2D = new Collider2D[5];


    public bool IsClimbing { set{_animator.SetBool(AnimatorKeys.IsClimbing, value);}}


    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _rigid = GetComponent<Rigidbody2D>();
      
    }

    public void Move(float direction)
    {
        _direction = direction;
        var isZero = _direction.IsNear(0);
        if(!isZero)
            _spriteRenderer.flipX = _direction < 0;

        var isWalking = !isZero;
        _animator.SetBool(AnimatorKeys.IsWalking, isWalking); 
    }

    public void Jump(bool isJumping)
    {
        _wantToJump = isJumping;
    }

    public void UseLadder()
    {

    }

    public void FixedUpdate()
    {
        var velocity = _rigid.velocity;
        velocity.x = Speed * _direction * Time.fixedDeltaTime;


        IsGrounded =  Physics2D.OverlapCircleNonAlloc(transform.position, _radiousCheck, _colliders2D, _goundMask) != 0;
        _animator.SetBool(AnimatorKeys.IsGrounded, IsGrounded);
        if(IsGrounded)
        {
            if (_wantToJump && !_isJumping)
            {
                _jumpTimer = 0;
                _isJumping = true;
                _rigid.gravityScale = 0;
                velocity.y = JumpForce * Time.fixedDeltaTime;
            }

        }



        if (!IsGrounded && _wantToJump && _isJumping && _jumpTimer < _jumpTime)
        {
            _jumpTimer += Time.deltaTime;
            
        }
        else
        {
            if(_isJumping)
            {
                _isJumping = false;
                _rigid.gravityScale = 1f;
            }
            
        }

        _rigid.velocity = velocity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, _radiousCheck);
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(_ladderTag))
        {
            AtLadder = true;
            _ladderConnections.Add(collision);
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(_ladderTag))
        {
            _ladderConnections.Remove(collision);
            if(_ladderConnections.Count == 0)
            {
                AtLadder = false;
            }
        }
    }
}
