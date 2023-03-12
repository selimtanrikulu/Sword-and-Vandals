using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public enum MovementState
{
    //Includes idle
    Move = 0,
    RollForward = 1,
    RollBackward = 2,
    RollRight = 3,
    RollLeft = 4,
    RollForwardLeft = 5,
    RollForwardRight = 6,
    RollBackwardLeft = 7,
    RollBackwardRight = 8,
    Stunned = -1,
    Died = -2,
}

public enum AttackState
{
    None = 0,
    Triple1 = 31,
    Triple2 = 32,
    Triple3 = 33,
    Double1 = 21,
    Double2 = 22,
    Single = 1,
    Block = -1,
}


public enum JumpState
{
    Grounded = 0,
    JumpStart = 1,
    Fall = 2,
}



public class CharacterStateController : MonoBehaviour
{
    private Animator _animator;

    private CharacterAnimationController _animationController;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animationController = GetComponent<CharacterAnimationController>();
        
        _hpBar = GetComponentInChildren<FillBar>();
        _maxHp = 100;
        _hp = _maxHp;
        _hpBar.UpdateBar(_hp,_maxHp);
    }


    private AttackState _attackState;
    public AttackState AttackState 
    {
        get { return _attackState; }
        set
        {
            _attackState = value;
            Debug.Log("attack state set to" + _attackState);
            if (_attackState == AttackState.None)
            {
                ActiveSkill = null;
                _animationController.StopWeaponTrail();
            }
        }
    }



    public Skill ActiveSkill { get; set; }

    public JumpState JumpState { get; set; }
    public AttackState WaitingAttackState { get; set; }
    public MovementState MovementState { get; set; }


    private float _maxHp;
    private float _hp;


    private FillBar _hpBar;

    public void ChangeHp(float amount)
    {
        _hp = Mathf.Max(0, _hp + amount);
        if(_hpBar)_hpBar.UpdateBar(_hp,_maxHp);


        //Character died
        if (_hp < 0.01f)
        {
            MovementState = MovementState.Died;
        }
    }

    public bool IsRolling()
    {
        return MovementState is
            MovementState.RollForward or
            MovementState.RollBackward or
            MovementState.RollLeft or
            MovementState.RollRight or
            MovementState.RollBackwardLeft or
            MovementState.RollBackwardRight or
            MovementState.RollForwardRight or
            MovementState.RollForwardLeft;
    }



    private void Update()
    {
        _animator.SetInteger("AttackState",(int)AttackState);
    }
    

}