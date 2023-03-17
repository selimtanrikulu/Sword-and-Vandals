using System;
using System.Collections.Generic;
using CartoonFX;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public enum EffectType
{
    Stun,
}


[Serializable]
public class SkillEffect
{
    public EffectType effectType;
    public float duration;



    public SkillEffect GetCopy()
    {
        SkillEffect copy = new SkillEffect
        {
            effectType = effectType,
            duration = duration
        };
        return copy;
    }
}



public class SkillImpact : MonoBehaviour,ICollider
{
    
    [HideInInspector] public ControllerBase creator;
    [SerializeField] private float startLifeTime;
    [SerializeField] private ParticleSystem collisionHitEffect;
    [SerializeField] public float baseDamage = 3;
    [SerializeField] private float lifeTimeAfterHit;

    
    [SerializeField] public List<SkillEffect> Effects = new List<SkillEffect>();

    //can be null
    [SerializeField] private GameObject skillEffectContainer;
    
    protected Collider Collider;


    protected virtual void Start()
    {
        Collider = GetComponent<Collider>();
    }


    protected virtual void Update()
    {
        if (startLifeTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            startLifeTime -= Time.deltaTime;
        }
    }

    public ColliderType GetColliderType()
    {
        return ColliderType.Impact;
    }



    public void HitOccurred()
    {
        SelfDestroy();
    }

    
    protected void SelfDestroy()
    {
        if(skillEffectContainer)skillEffectContainer.gameObject.SetActive(false);
        collisionHitEffect.gameObject.SetActive(true);
        HandleCameraShake();
        collisionHitEffect.Play();
        startLifeTime = lifeTimeAfterHit;

        if (Collider)
        {
            Collider.enabled = false;
        }

        if (this is Projectile projectile)
        {
            projectile.projectileSpeed = 0;
        }

        if (this is Arc arc)
        {
            arc.projectileSpeed = 0;
        }
    }

    private void HandleCameraShake()
    {
        if (collisionHitEffect.TryGetComponent(out CFXR_Effect effect))
        {
            if (creator is AIController)
            {
                effect.cameraShake.enabled = false;
            }
            else if(creator is PlayerControl)
            {
                effect.cameraShake.enabled = true;
            }
        }
    }
    
}
