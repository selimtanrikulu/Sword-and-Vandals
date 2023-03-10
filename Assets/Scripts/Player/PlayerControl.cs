using System;
using UnityEngine;


public enum AttackState
{
    None = 0,
    Attack1_1 = 11,
    Attack1_2 = 12,
    Attack1_3 = 13,
    Attack2 = 3,
    Block = -1,
}


public enum MovementState
{
    //Includes idle
    Move = 0,
    RollForward = 1,
    RollBackward = 2,
    RollRight = 3,
    RollLeft = 4,
    Stunned = 5,
}

public enum JumpState
{
    Grounded = 0,
    JumpStart = 1,
    Fall = 2,
}


[Serializable]
public enum DodgeType
{
    Forward,
    Backward,
    Left,
    Right,
}


public class PlayerControl : MonoBehaviour
{
    private CharacterController _characterController;

    [SerializeField] private float dodgingMovementSpeed;
    [SerializeField] private float runningMovementSpeed;


    private float _movementSpeed;
    
    
    [SerializeField] private float jumpStartVelocity;
    [SerializeField] private float gravity;

    private float _yVelocity;
    
    private Animator _animator;

    private float _horizontal;
    private float _vertical;


    [SerializeField] private float rotationSpeed;
    [SerializeField] private float fallStartYVelocity;

    private AttackState _attackState;
    private MovementState _movementState;
    private JumpState _jumpState;
    private AttackState _waitingAttack;


    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        _movementSpeed = runningMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleRotation();
        HandleAttackAnimation();
        HandleMovementAnimation();
        HandleJumpAnimation();
    }

    private void HandleRotation()
    {
        if (Mathf.Abs(Input.GetAxis("Mouse X")) > 0.1f)
        {
            transform.Rotate(Vector3.up,rotationSpeed * Time.deltaTime * Input.GetAxis("Mouse X"));
        }

    }

    private void GetStunned()
    {
        if (_movementState is MovementState.RollBackward or MovementState.RollForward or MovementState.RollRight or MovementState.RollLeft) return;
        
        
        _movementState = MovementState.Stunned;
        _attackState = AttackState.None;
    }
    
    private void Move()
    {
        Vector3 moveDist = new Vector3();

        switch (_movementState)
        {
            case MovementState.Move:
                _horizontal = Input.GetAxis("Horizontal");
                _vertical =Input.GetAxis("Vertical");
                Transform myTransform = transform;
                Vector3 verticalMove = myTransform.forward * (_vertical * runningMovementSpeed * Time.deltaTime);
                Vector3 horizontalMove = myTransform.right * (_horizontal * runningMovementSpeed * Time.deltaTime);
                moveDist = verticalMove + horizontalMove;
                break;
            
            case MovementState.RollForward:
                moveDist =  transform.forward * (_movementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollBackward:
                moveDist = -transform.forward * (_movementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollRight:
                moveDist = transform.right * (_movementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollLeft:
                moveDist = -transform.right * (_movementSpeed * Time.deltaTime) ;
                break;
            
            
            case MovementState.Stunned:
                //nothing
                break;
            
        }

        //Apply gravity
        if (!_characterController.isGrounded)
        {
            _yVelocity += gravity * Time.deltaTime;
        }
        
        
        
        moveDist += Vector3.up * (Time.deltaTime * _yVelocity);

        _characterController.Move(moveDist);

    }


    private void DodgeDone()
    {
        _movementState = MovementState.Move;
        _movementSpeed = runningMovementSpeed;
    }

    private void DodgeStarted(DodgeType dodgeType)
    {
        _movementSpeed = dodgingMovementSpeed;
        return;
        switch (dodgeType)
        {
            case DodgeType.Forward:
                _movementState = MovementState.RollForward;
                break;
            
            case DodgeType.Backward:
                _movementState = MovementState.RollBackward;
                break;
        }
        
    }

    
    private void AttackOccurred()
    {
        //TODO
    }
    private void HandleAttackAnimation()
    {
        if (_movementState != MovementState.Move)
        {
            _attackState = AttackState.None;
            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_attackState == AttackState.None)
            {
                _attackState = AttackState.Attack1_1;
            }
            else if (_attackState == AttackState.Attack1_1)
            {
                _waitingAttack = AttackState.Attack1_2;
            }
            else if (_attackState == AttackState.Attack1_2)
            {
                _waitingAttack = AttackState.Attack1_3;
            }
        }
        
        
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_attackState == AttackState.None)
            {
                _attackState = AttackState.Attack2;
            }
        }
        
        
        
         if(Input.GetKey(KeyCode.Mouse1))
        { 
            if(_movementState == MovementState.Move && _attackState == AttackState.None)
            {
                _attackState = AttackState.Block;
            }
        }
        else
        {
            if (_attackState == AttackState.Block)
            {
                _attackState = AttackState.None;
            }
        }
        
        
        

        _animator.SetInteger("AttackState",(int)_attackState);
    }

    private void HandleJumpAnimation()
    {
        if (_characterController.isGrounded)
        {
            _jumpState = JumpState.Grounded;
            
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (_attackState == AttackState.None && _movementState == MovementState.Move)
                {
                    _jumpState = JumpState.JumpStart;
                    _yVelocity = jumpStartVelocity;
                }
            }
        }
        else
        {
            if (fallStartYVelocity > _characterController.velocity.y)
            {
                _jumpState = JumpState.Fall;
            }
        }

        _animator.SetInteger("JumpState",(int)_jumpState);
            
    }
    
    private void HandleMovementAnimation()
    {
        if (_characterController.isGrounded)
        {
            if (_movementState != MovementState.Stunned)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && _movementState == MovementState.Move)
                {
                    if (Input.GetAxisRaw("Vertical") > 0.3f)
                    {
                        _movementState = MovementState.RollForward;
                    }
                    else if(Input.GetAxisRaw("Horizontal") > 0.3f)
                    {
                        _movementState = MovementState.RollRight;
                    }
                    else if(Input.GetAxisRaw("Horizontal") < -0.3f)
                    {
                        _movementState = MovementState.RollLeft;
                    }
                    else if(Input.GetAxisRaw("Vertical") < -0.3f)
                    {
                        _movementState = MovementState.RollBackward;
                    }
                    else
                    {
                        _movementState = MovementState.RollForward;
                    }
                    
                    _attackState = AttackState.None;
                }
                
                
            }
            
        }


        //for test
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            GetStunned();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _movementState = MovementState.Move;
        }
        //-----
        
        
        
        _animator.SetInteger("MovementState",(int)_movementState);
        //used for blend tree
        _animator.SetFloat("x",_horizontal);
        _animator.SetFloat("y",_vertical);
    }

    
    private void AttackFinished()
    {
        _attackState = _waitingAttack;
        _waitingAttack = AttackState.None;
    }
}
