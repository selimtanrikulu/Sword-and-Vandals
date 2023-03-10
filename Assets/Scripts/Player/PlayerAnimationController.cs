using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private CharacterController _characterController;
    private PlayerControl _playerController;
    private PlayerStateController _stateController;
    private Animator _animator;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _playerController = GetComponent<PlayerControl>();
        _stateController = GetComponent<PlayerStateController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttackAnimation();
        HandleMovementAnimation();
        HandleJumpAnimation();
    }

    // notifier calls
    private void DodgeDone()
    {
        _stateController.movementState = MovementState.Move;
        _playerController.MovementSpeed = MovementConfig.runningMovementSpeed;
    }

    // notifier calls
    private void DodgeStarted(DodgeType dodgeType)
    {
        _playerController.MovementSpeed = MovementConfig.dodgingMovementSpeed;
        return;
        switch (dodgeType)
        {
            case DodgeType.Forward:
                _stateController.movementState = MovementState.RollForward;
                break;
            
            case DodgeType.Backward:
                _stateController.movementState = MovementState.RollBackward;
                break;
        }
        
    }

    
    // notifier calls
    private void AttackOccurred()
    {
        //TODO
    }
    private void HandleAttackAnimation()
    {
        if (_stateController.movementState != MovementState.Move)
        {
            _stateController.attackState = AttackState.None;
            return;
        }
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_stateController.attackState == AttackState.None)
            {
                _stateController.attackState = AttackState.Attack1_1;
            }
            else if (_stateController.attackState == AttackState.Attack1_1)
            {
                _stateController.waitingAttackState = AttackState.Attack1_2;
            }
            else if (_stateController.attackState == AttackState.Attack1_2)
            {
                _stateController.waitingAttackState = AttackState.Attack1_3;
            }
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            if (_stateController.attackState == AttackState.None)
            {
                _stateController.attackState = AttackState.Attack2;
            }
        }
        if(Input.GetKey(KeyCode.Mouse1))
        { 
            if(_stateController.movementState == MovementState.Move && _stateController.attackState == AttackState.None)
            {
                _stateController.attackState = AttackState.Block;
            }
        }
        else
        {
            if (_stateController.attackState == AttackState.Block)
            {
                _stateController.attackState = AttackState.None;
            }
        }
        _animator.SetInteger("AttackState",(int)_stateController.attackState);
    }

    private void HandleJumpAnimation()
    {
        if (_characterController.isGrounded)
        {
            _stateController.jumpState = JumpState.Grounded;
            
            
            if(Input.GetKeyDown(KeyCode.Space))
            {
                if (_stateController.attackState == AttackState.None && _stateController.movementState == MovementState.Move)
                {
                    _stateController.jumpState = JumpState.JumpStart;
                    _playerController.yVelocity = MovementConfig.jumpStartVelocity;
                }
            }
        }
        else
        {
            if (MovementConfig.fallStartYVelocity > _characterController.velocity.y)
            {
                _stateController.jumpState = JumpState.Fall;
            }
        }

        _animator.SetInteger("JumpState",(int)_stateController.jumpState);
            
    }
    
    private void HandleMovementAnimation()
    {
        if (_characterController.isGrounded)
        {
            if (_stateController.movementState != MovementState.Stunned)
            {
                if (Input.GetKeyDown(KeyCode.LeftShift) && _stateController.movementState == MovementState.Move)
                {
                    if (Input.GetAxisRaw("Vertical") > 0.3f)
                    {
                        _stateController.movementState = MovementState.RollForward;
                    }
                    else if(Input.GetAxisRaw("Horizontal") > 0.3f)
                    {
                        _stateController.movementState = MovementState.RollRight;
                    }
                    else if(Input.GetAxisRaw("Horizontal") < -0.3f)
                    {
                        _stateController.movementState = MovementState.RollLeft;
                    }
                    else if(Input.GetAxisRaw("Vertical") < -0.3f)
                    {
                        _stateController.movementState = MovementState.RollBackward;
                    }
                    else
                    {
                        _stateController.movementState = MovementState.RollForward;
                    }
                    
                    _stateController.attackState = AttackState.None;
                }
            }
            
        }

        //for test
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            _playerController.GetStunned();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _stateController.movementState = MovementState.Move;
        }
        //-----
        
        _animator.SetInteger("MovementState",(int)_stateController.movementState);
        //used for blend tree
        _animator.SetFloat("x",_playerController.horizontal);
        _animator.SetFloat("y",_playerController.vertical);
    }
    
    // notifier calls
    private void AttackFinished()
    {
        _stateController.attackState = _stateController.waitingAttackState;
        _stateController.waitingAttackState = AttackState.None;
    }
}
