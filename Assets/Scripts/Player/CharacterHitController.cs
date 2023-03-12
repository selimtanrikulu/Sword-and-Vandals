using CartoonFX;
using UnityEngine;


public enum ColliderType
{
    Impact,
    
}

public interface ICollider
{
    public ColliderType GetColliderType();
}

public class CharacterHitController : MonoBehaviour
{
    private ControllerBase _controllerBase;
    private CharacterStateController _stateController;
    private CharacterAnimationController _animationController;

    private void Start()
    {
        _controllerBase = GetComponent<ControllerBase>();
        _stateController = GetComponent<CharacterStateController>();
        _animationController = GetComponent<CharacterAnimationController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SkillImpact skillImpact))
        {
            if (skillImpact.creator == null)
            {
                Debug.LogError("Creator is not assigned !");
            }
            
            if (skillImpact.creator != null && skillImpact.creator != _controllerBase)
            {
                if (_stateController.IsRolling())
                {
                    //player dodged attack
                }
                else
                {
                    HandleCameraShake(skillImpact);
                    skillImpact.collisionHitEffect.gameObject.SetActive(true);
                    skillImpact.collisionHitEffect.Play();
                    _animationController.PlayImpactAnimation();
                    _stateController.ChangeHp(-skillImpact.baseDamage);
                }
            }
        }
    }


    private void HandleCameraShake(SkillImpact skillImpact)
    {
        if (skillImpact.collisionHitEffect.TryGetComponent(out CFXR_Effect effect))
        {
            if (skillImpact.creator is AIController)
            {
                effect.cameraShake.enabled = false;
            }
            else if(skillImpact.creator is PlayerControl)
            {
                effect.cameraShake.enabled = true;
            }
        }
    }
    

    
}
