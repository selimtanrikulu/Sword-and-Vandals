using UnityEngine;

public class Projectile : SkillImpact
{
    [HideInInspector] public Vector3 targetPosition;
    [SerializeField] public float projectileSpeed;

    private Rigidbody _rb;

    protected override void Start()
    {
        base.Start();


        _rb = GetComponentInChildren<Rigidbody>();
    }

    new void Update()
    {
        base.Update();

        if (_rb)
        {
            
            
            Vector3 dir = (targetPosition - transform.position);
            if (dir.magnitude < 3 && projectileSpeed > 50) projectileSpeed = 25;
            dir.Normalize();
            _rb.velocity = dir * projectileSpeed;

        }
    }

}
