using UnityEngine;

public class SkillImpact : MonoBehaviour,ICollider
{
    public ControllerBase creator;
    [SerializeField] private float lifeTime;
    [SerializeField] public ParticleSystem collisionHitEffect;

    private void Update()
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
