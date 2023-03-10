using System;
using UnityEngine;
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
        if (_movementState == MovementState.Move)
        {
            _horizontal = Input.GetAxis("Horizontal");
            _vertical =Input.GetAxis("Vertical");
            Transform myTransform = transform;
            Vector3 verticalMove = myTransform.forward * (_vertical * runningMovementSpeed);
            Vector3 horizontalMove = myTransform.right * (_horizontal * runningMovementSpeed);
            _characterController.SimpleMove(verticalMove + horizontalMove);
        }
        else if(_movementState == MovementState.RollForward)
        {
            Vector3 moveDir = transform.forward;
            Vector3 moveDist = moveDir * dodgingMovementSpeed;
            _characterController.SimpleMove(moveDist);
        }
        else if (_movementState == MovementState.RollBackward)
        {
            Vector3 moveDir = transform.forward * -1;
            Vector3 moveDist = moveDir * dodgingMovementSpeed;
            _characterController.SimpleMove(moveDist);
        }
        else if(_movementState == MovementState.RollRight)
        {
            Vector3 moveDir = transform.right;
            Vector3 moveDist = moveDir * dodgingMovementSpeed;
            _characterController.SimpleMove(moveDist);
        }
        else if (_movementState == MovementState.RollLeft)
        {
            Vector3 moveDir = transform.right * -1;
            Vector3 moveDist = moveDir * dodgingMovementSpeed;
            _characterController.SimpleMove(moveDist);
        }
        else if(_movementState == MovementState.Stunned)
        {
            //nothing
        }


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
    
    private void HandleMovementAnimation()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && _movementState != MovementState.Stunned)
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
        
        //for tes
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            GetStunned();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            _movementState = MovementState.Move;
        }
        
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
