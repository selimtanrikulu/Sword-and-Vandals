using System;
using DG.Tweening;
using UnityEngine;

public class Arc : SkillImpact
{
    [HideInInspector] public Vector3 targetPosition;
    private Vector3 _startPosition;
    [SerializeField] public float projectileSpeed;

    private Vector3 _xzStartPos;
    private Vector3 _xzTargetPos;
    private float _r;
    private float _startDeltaY;
    
    
    new void Start()
    {
        base.Start();

        var myTransform = transform;
        _startPosition = myTransform.position;
        _xzTargetPos = targetPosition;
        _xzTargetPos.y = 0;
        _xzStartPos = _startPosition;
        _xzStartPos.y = 0;
        _r = (_xzTargetPos - _xzStartPos).magnitude / 2;
        _startDeltaY = targetPosition.y - _startPosition.y;


        
        //Handle lifetime rotation
        var eulerAngles = myTransform.eulerAngles;
        eulerAngles = new Vector3(-90, eulerAngles.y, eulerAngles.z);
        myTransform.eulerAngles = eulerAngles;
        float duration = (_xzTargetPos - _xzStartPos).magnitude / projectileSpeed;
        transform.DORotate(eulerAngles + new Vector3(160,0,0), duration);
        //----------------------------------------------------
    }

    new void Update()
    {
        base.Update();
        
        Move();
    }

    private void Move()
    {
        var position = transform.position;

        
        //Arc finished
        if ((position - targetPosition).magnitude < 1f)
        {
            //Not destroyed yet
            if (Collider.enabled)
            {
                SelfDestroy();
            }
            return;
        }
        
        
        
        Vector3 pos = position;
        Vector3 xzPos = position;
        xzPos.y = 0;
        Vector3 dir = (_xzTargetPos - xzPos).normalized;
        pos += dir * (Time.deltaTime * projectileSpeed);
        float a = (xzPos-_xzStartPos).magnitude;
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
