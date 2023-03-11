using System;
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

    private void Start()
    {
        _controllerBase = GetComponent<ControllerBase>();
        _stateController = GetComponent<CharacterStateController>();
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
                skillImpact.collisionHitEffect.Play();
                _stateController.ImpactState = ImpactState.Impact;
            }
        }
    }

    
}
