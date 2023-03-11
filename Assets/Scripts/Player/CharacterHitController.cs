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

    private void Start()
    {
        _controllerBase = GetComponent<ControllerBase>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out SkillImpact skillImpact))
        {
            if (skillImpact.creator == null)
            {
                Debug.LogError("Creator could not assign self");
            }
            
            if (skillImpact.creator != null && skillImpact.creator != _controllerBase)
            {
                skillImpact.collisionHitEffect.Play();
            }
        }
    }

    
}
