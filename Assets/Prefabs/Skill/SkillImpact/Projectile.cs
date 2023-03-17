using UnityEngine;

public class Projectile : SkillImpact
{
    [HideInInspector] public Vector3 targetPosition;
    [SerializeField] public float projectileSpeed;
    
    new void Update()
    {
        base.Update();
        Transform myTransform = transform;
        Vector3 pos = myTransform.position;

        Vector3 dir = (targetPosition - pos).normalized;
        pos += dir * (Time.deltaTime * projectileSpeed);
        myTransform.position = pos;
    }

}
