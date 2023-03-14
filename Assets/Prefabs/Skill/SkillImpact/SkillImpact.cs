using UnityEngine;

public class SkillImpact : MonoBehaviour,ICollider
{
    
    [HideInInspector] public ControllerBase creator;
    [SerializeField] private float lifeTime;
    [SerializeField] public ParticleSystem collisionHitEffect;


    [SerializeField] public float baseDamage = 3;
    
    protected virtual void Update()
    {
        if (lifeTime < 0)
        {
            Destroy(gameObject);
        }
        else
        {
            lifeTime -= Time.deltaTime;
        }
    }

    public ColliderType GetColliderType()
    {
        return ColliderType.Impact;
    }
}
