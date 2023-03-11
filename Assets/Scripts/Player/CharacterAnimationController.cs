using System.Linq;
using UnityEditor.Animations;
using UnityEngine;


public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Skill skill;

    private CharacterController _characterController;
    private ControllerBase _controllerBase;
    private CharacterStateController _stateController;
    private Animator _animator;

    [SerializeField] private GameObject rightHandWeaponHitLocation;
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _controllerBase = GetComponent<ControllerBase>();
        _stateController = GetComponent<CharacterStateController>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        HandleAttackAnimation();
        HandleMovementAnimation();
        HandleJumpAnimation();
    }



    public void PlayImpactAnimation()
    {
        _animator.Play("GetImpact", 2, 0f);
    }


    // notifier calls
    private void DodgeStarted()
    {
        _controllerBase.MovementSpeed = MovementConfig.DodgingMovementSpeed;
    }


    // notifier calls
    private void AttackOccurred()
    {
        if(rightHandWeaponHitLocation == null) return;
        
        GameObject skillImpactGameObject = Instantiate(skill.skillImpact,rightHandWeaponHitLocation.transform.position,Quaternion.identity);
        skillImpactGameObject.GetComponent<SkillImpact>().creator = _controllerBase;
    }

    private AnimatorState GetAnimatorState(string stateName)
    {
        AnimatorController animatorController = _animator.runtimeAnimatorController as AnimatorController;
        if (animatorController != null)
        {
            AnimatorControllerLayer[] acLayers =animatorController.layers;
            AnimatorControllerLayer layer = acLayers.FirstOrDefault(x => x.name == "AttackLayer");
            if (layer != null)
            {
                ChildAnimatorState[] animStates = layer.stateMachine.states;
                foreach (ChildAnimatorState j in animStates)
                {
                    if (j.state.name == stateName)
                    {
                        return j.state;
                    }
                }
            }
        }
        return null;
    }

    private void HandleAttackAnimation()
    {
        if (_stateController.MovementState != MovementState.Move)
        {
            _stateController.AttackState = AttackState.None;
            return;
        }

        if (_controllerBase.Attack1Input)
        {
            if (_stateController.AttackState == AttackState.None)
            {
                _stateController.AttackState = AttackState.Attack1_1;
            }
            else if (_stateController.AttackState == AttackState.Attack1_1)
            {
                _stateController.WaitingAttackState = AttackState.Attack1_2;
            }
            else if (_stateController.AttackState == AttackState.Attack1_2)
            {
                _stateController.WaitingAttackState = AttackState.Attack1_3;
            }
        }
        else if (_controllerBase.Attack2Input)
        {
            AnimatorState state = GetAnimatorState("SingleAttack");
            if (state)
            {
                state.motion = skill.attackAnimation;
                if (_stateController.AttackState == AttackState.None)
                {
                    _stateController.AttackState = AttackState.Attack2;
                }
            }
        }

        if (_controllerBase.BlockInput)
        {
            if (_stateController.MovementState == MovementState.Move &&
                _stateController.AttackState == AttackState.None)
            {
                _stateController.AttackState = AttackState.Block;
            }
        }
        else
        {
            if (_stateController.AttackState == AttackState.Block)
            {
                _stateController.AttackState = AttackState.None;
            }
        }

        _animator.SetInteger("AttackState", (int)_stateController.AttackState);
    }

    private void HandleJumpAnimation()
    {
        if (_characterController.isGrounded)
        {
            _stateController.JumpState = JumpState.Grounded;


            if (_controllerBase.JumpInput)
            {
                if (_stateController.AttackState == AttackState.None &&
                    _stateController.MovementState == MovementState.Move)
                {
                    _stateController.JumpState = JumpState.JumpStart;
                    _controllerBase.YVelocity = MovementConfig.JumpStartVelocity;
                }
            }
        }
        else
        {
            if (MovementConfig.FallStartYVelocity > _characterController.velocity.y)
            {
                _stateController.JumpState = JumpState.Fall;
            }
        }

        _animator.SetInteger("JumpState", (int)_stateController.JumpState);
    }

    // notifier calls
    private void DodgeDone()
    {
        _stateController.MovementState = MovementState.Move;
        _controllerBase.MovementSpeed = MovementConfig.RunningMovementSpeed;
    }

    private void HandleMovementAnimation()
    {
        if (_controllerBase.RollInput && _characterController.isGrounded &&
            _stateController.MovementState != MovementState.Stunned &&
            _stateController.MovementState == MovementState.Move)
        {
            if (_controllerBase.Vertical > 0.3f)
            {
                _stateController.MovementState = MovementState.RollForward;
            }
            else if (_controllerBase.Horizontal > 0.3f)
            {
                _stateController.MovementState = MovementState.RollRight;
            }
            else if (_controllerBase.Horizontal < -0.3f)
            {
                _stateController.MovementState = MovementState.RollLeft;
            }
            else if (_controllerBase.Vertical < -0.3f)
            {
                _stateController.MovementState = MovementState.RollBackward;
            }
            else
            {
                _stateController.MovementState = MovementState.RollForward;
            }

            _stateController.AttackState = AttackState.None;
        }

        //for test
        if (_controllerBase.GetStunInputTest)
        {
            GetStunned();
        }

        if (_controllerBase.BreakStunInputTest)
        {
            _stateController.MovementState = MovementState.Move;
        }
        //-----

        _animator.SetInteger("MovementState", (int)_stateController.MovementState);
        //used for blend tree
        _animator.SetFloat("x", _controllerBase.Horizontal);
        _animator.SetFloat("y", _controllerBase.Vertical);
    }

    public void GetStunned()
    {
        if (_stateController.MovementState is MovementState.RollBackward or MovementState.RollForward
            or MovementState.RollRight or MovementState.RollLeft) return;

        _stateController.MovementState = MovementState.Stunned;
        _stateController.AttackState = AttackState.None;
    }

    // notifier calls
    private void AttackFinished()
    {
        _stateController.AttackState = _stateController.WaitingAttackState;
        _stateController.WaitingAttackState = AttackState.None;
    }
}