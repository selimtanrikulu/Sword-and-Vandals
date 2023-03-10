using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;


public enum AttackState
{
    None = 0,
    Attack1 = 1,
    Block = 2,



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
    JumpStart = 6,
    Fall = 7,
    JumpEnd = 8,
    
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

    
    [SerializeField] private float jumpStartVelocity;
    [SerializeField] private float gravity;

    private float _yVelocity;
    
    private Animator _animator;

    private float _horizontal;
    private float _vertical;


    [SerializeField] private float rotationSpeed;

    private AttackState _attackState;
    private MovementState _movementState;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        HandleRotation();
        HandleAttackAnimation();
        HandleMovementAnimation();
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

                
            case MovementState.Move or MovementState.Fall or MovementState.JumpStart or MovementState.JumpEnd:
                _horizontal = Input.GetAxis("Horizontal");
                _vertical =Input.GetAxis("Vertical");
                Transform myTransform = transform;
                Vector3 verticalMove = myTransform.forward * (_vertical * runningMovementSpeed * Time.deltaTime);
                Vector3 horizontalMove = myTransform.right * (_horizontal * runningMovementSpeed * Time.deltaTime);
                moveDist = verticalMove + horizontalMove;

                break;
            
            
            
            case MovementState.RollForward:
                moveDist =  transform.forward * (dodgingMovementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollBackward:
                moveDist = -transform.forward * (dodgingMovementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollRight:
                moveDist = transform.right * (dodgingMovementSpeed * Time.deltaTime);
                break;
            
            case MovementState.RollLeft:
                moveDist = -transform.right * (dodgingMovementSpeed * Time.deltaTime) ;
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
    }

    private void DodgeStarted(DodgeType dodgeType)
    {
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
        if (Input.GetKeyDown(KeyCode.Mouse0) && _movementState == MovementState.Move)
        {
            _attackState = AttackState.Attack1;
        }

        
        
        if(Input.GetKey(KeyCode.Mouse1))
        { 
            if(_movementState == MovementState.Move)
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

    private void JumpStartFinished()
    {
        if (_characterController.isGrounded)
        {
            _movementState = MovementState.JumpEnd;
        }
        else
        {
            _movementState = MovementState.Fall;
        }
    }


    private void JumpEndFinished()
    {
        _movementState = MovementState.Move;
    }
    
    private void HandleMovementAnimation()
    {
        if (_characterController.isGrounded)
        {
            if (_movementState != MovementState.Stunned)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    if (Input.GetAxisRaw("Vertical") > 0.9f)
                    {
                        _movementState = MovementState.RollForward;
                    }
                    else if(Input.GetAxisRaw("Vertical") < -0.9f)
                    {
                        _movementState = MovementState.RollBackward;
                    }
                    else if(Input.GetAxisRaw("Horizontal") > 0.9f)
                    {
                        _movementState = MovementState.RollRight;
                    }
                    else if(Input.GetAxisRaw("Horizontal") < -0.9f)
                    {
                        _movementState = MovementState.RollLeft;
                    }
                    _attackState = AttackState.None;
                }
                
                else if(Input.GetKeyDown(KeyCode.Space))
                {

                    _movementState = MovementState.JumpStart;
                    _yVelocity = jumpStartVelocity;
                    _attackState = AttackState.None;
                    
                }
                else if(_movementState == MovementState.Fall)
                {
                    _movementState = MovementState.JumpEnd;
                }
                else if(_movementState == MovementState.JumpStart)
                {
                    _movementState = MovementState.JumpEnd;
                }
            }
            
        }
        else
        {
            if (_movementState != MovementState.JumpStart)
            {
                _movementState = MovementState.Fall;
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
        _attackState = AttackState.None;
    }
}
