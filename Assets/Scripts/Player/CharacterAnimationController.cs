using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;


public class CharacterAnimationController : MonoBehaviour
{
    private Skill _basicSkill;
    private Skill _skill1;
    private Skill _skill2;
    private Skill _skill3;


    private CharacterController _characterController;
    private ControllerBase _controllerBase;
    private CharacterStateController _stateController;
    private Animator _animator;

    [SerializeField] private GameObject rightHandWeaponHitLocation;
    [SerializeField] private ParticleSystem weaponTrail;
    private ISkillManager _skillManager;

    [Inject]
    private void Inject(ISkillManager skillManager)
    {
        _skillManager = skillManager;
    }

    
    void Start()
    {
        _basicSkill = _skillManager.GetSkillByIndex(0);
        _skill1 = _skillManager.GetSkillByIndex(1);
        _skill2 = _skillManager.GetSkillByIndex(2);
        _skill3 = _skillManager.GetSkillByIndex(3);
        
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
        _animator.Play("GetImpact", 1, 0f);
    }


    // notifier calls
    private void DodgeStarted()
    {
        _controllerBase.MovementSpeed = MovementConfig.DodgingMovementSpeed;
    }

    // notifier calls
    public void StartWeaponTrail()
    {
        weaponTrail.Play();
    }
    // notifier calls
    public void StopWeaponTrail()
    {
        weaponTrail.Stop();
    }

    // notifier calls
    private void AttackOccurred()
    {
        if(rightHandWeaponHitLocation == null || _stateController.ActiveSkill == null) return;
        
        GameObject skillImpactGameObject = Instantiate(_stateController.ActiveSkill.GetCurrentImpact(),rightHandWeaponHitLocation.transform.position,Quaternion.identity);
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

    private void FillAnimatorAnimations(Skill skill)
    {
        if (skill is TripleSkill tripleSkill)
        {
            AnimatorState triple1 = GetAnimatorState("Triple-1");
            if (triple1)
            {
                triple1.motion = tripleSkill.attackAnimation1;
            }
            
            AnimatorState triple2 = GetAnimatorState("Triple-2");
            if (triple2)
            {
                triple2.motion = tripleSkill.attackAnimation2;
            }
            
            AnimatorState triple3 = GetAnimatorState("Triple-3");
            if (triple3)
            {
                triple3.motion = tripleSkill.attackAnimation3;
            }
            
        }
        
        else if (skill is DoubleSkill doubleSkill)
        {
            AnimatorState double1 = GetAnimatorState("Double-1");
            if (double1)
            {
                double1.motion = doubleSkill.attackAnimation1;
            }
            
            AnimatorState double2 = GetAnimatorState("Double-2");
            if (double2)
            {
                double2.motion = doubleSkill.attackAnimation2;
            }
        }
        
        else if (skill is SingleSkill singleSkill)
        {
            AnimatorState single = GetAnimatorState("Single");
            if (single)
            {
                single.motion = singleSkill.attackAnimation;
            }
        }
        else
        {
            Debug.LogError("Unknown skill type !");
        }
    }
    
    private void SkillInputArrived(Skill skill)
    {
        if(_stateController.ActiveSkill != null && _stateController.ActiveSkill != skill)return;


        if (skill is TripleSkill tripleSkill)
        {
            if (_stateController.AttackState == AttackState.None)
            {
                _stateController.ActiveSkill = skill;
                FillAnimatorAnimations(skill);
                _stateController.AttackState = AttackState.Triple1;
            }
            else if (_stateController.AttackState == AttackState.Triple1)
            {
                _stateController.WaitingAttackState = AttackState.Triple2;
            }
            else if (_stateController.AttackState == AttackState.Triple2)
            {
                _stateController.WaitingAttackState = AttackState.Triple3;
            }
        }
        else if (skill is DoubleSkill doubleSkill)
        {
            if (_stateController.AttackState == AttackState.None)
            {
                _stateController.ActiveSkill = skill;
                FillAnimatorAnimations(skill);
                _stateController.AttackState = AttackState.Double1;
            }
            else if (_stateController.AttackState == AttackState.Double1)
            {
                _stateController.WaitingAttackState = AttackState.Double2;
            }
        }
        else if (skill is SingleSkill singleSkill)
        {
            if (_stateController.AttackState == AttackState.None)
            {
                _stateController.ActiveSkill = skill;
                FillAnimatorAnimations(skill);
                _stateController.AttackState = AttackState.Single;
            }
        }
    }
    
    private void HandleAttackAnimation()
    {
        if (_stateController.MovementState != MovementState.Move)
        {
            _stateController.AttackState = AttackState.None;
            return;
        }

        if (_controllerBase.BasicAttackInput)
        {
            SkillInputArrived(_basicSkill);
        }
        else if (_controllerBase.Skill1Input)
        {
            SkillInputArrived(_skill1);   
        }
        else if (_controllerBase.Skill2Input)
        {
            SkillInputArrived(_skill2);
        }
        else if (_controllerBase.Skill3Input)
        {
            SkillInputArrived(_skill3);
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
            if (_controllerBase.VerticalRaw > 0.99f)
            {
                if (_controllerBase.HorizontalRaw > 0.99f)
                {
                    _stateController.MovementState = MovementState.RollForwardRight;
                }
                else if (_controllerBase.HorizontalRaw < -0.99f)
                {
                    _stateController.MovementState = MovementState.RollForwardLeft;
                }
                else
                {
                    _stateController.MovementState = MovementState.RollForward;
                }
            }
            else if (_controllerBase.VerticalRaw < -0.99f)
            {
                if (_controllerBase.HorizontalRaw > 0.99f)
                {
                    _stateController.MovementState = MovementState.RollBackwardRight;
                }
                else if (_controllerBase.HorizontalRaw < -0.99f)
                {
                    _stateController.MovementState = MovementState.RollBackwardLeft;
                }
                else
                {
                    _stateController.MovementState = MovementState.RollBackward;
                }
            }
            else
            {
                if (_controllerBase.HorizontalRaw > 0.99f)
                {
                    _stateController.MovementState = MovementState.RollRight;
                }
                else if (_controllerBase.HorizontalRaw < -0.99f)
                {
                    _stateController.MovementState = MovementState.RollLeft;
                }
                else
                {
                    _stateController.MovementState = MovementState.RollForward;
                }
            }
            

            _stateController.AttackState = AttackState.None;
            _stateController.WaitingAttackState = AttackState.None;
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






        if (_stateController.MovementState == MovementState.RollForward)
        {
            AnimatorState animatorState = GetAnimatorState("RollForward");

        }
        
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