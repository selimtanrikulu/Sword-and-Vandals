using System;
using UnityEngine;

public class Arc : SkillImpact
{
    [HideInInspector] public Vector3 targetPosition;
    private Vector3 _startPosition;
    [SerializeField] private float projectileSpeed;

    private Vector3 xzStartPos;
    private Vector3 xzTargetPos;
    private float _r;


    private float _startDeltaY;
    
    private void Start()
    {
        _startPosition = transform.position;
        xzTargetPos = targetPosition;
        xzTargetPos.y = 0;
        xzStartPos = _startPosition;
        xzStartPos.y = 0;
        _r = (xzTargetPos - xzStartPos).magnitude / 2;
        _startDeltaY = targetPosition.y - _startPosition.y;
    }

    new void Update()
    {
        base.Update();

        Vector3 pos = transform.position;
        
        Vector3 xzPos = transform.position;
        xzPos.y = 0;
        
        
        Vector3 dir = (xzTargetPos - xzPos).normalized;
        
        
        pos += dir * (Time.deltaTime * projectileSpeed);


        
       

        float a = (xzPos-xzStartPos).magnitude;
        float Q = Mathf.Acos((_r - a) / _r);
        float y = Mathf.Sin(Q)*_r;


        y = Math.Min(100, y);
        
        if (y is not Single.NaN)
        {
            pos.y = y + _startPosition.y;
        }

        float h = (a * _startDeltaY) / (Mathf.Sqrt(4 * _r * _r - _startDeltaY * _startDeltaY) + a);


        h = Mathf.Min(100, h);
        
        if (h is not Single.NaN)
        {
            pos.y += h;
        }
      
        transform.position = pos;
    }

    
}
