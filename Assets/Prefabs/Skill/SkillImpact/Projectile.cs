using System;
using UnityEngine;

public class Projectile : SkillImpact
{
    


    [HideInInspector] public Vector3 dir;
    [SerializeField] private float projectileSpeed;
    
    new void Update()
    {
        base.Update();
        var myTransform = transform;
        Vector3 pos = myTransform.position;
        pos += dir * (Time.deltaTime * projectileSpeed);
        myTransform.position = pos;
    }


    public void ResetSpeed()
    {
        projectileSpeed = 0;
    }
    
}
