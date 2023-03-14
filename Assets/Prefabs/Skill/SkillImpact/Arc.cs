using UnityEngine;

public class Arc : SkillImpact
{
    [SerializeField] private float deltaHeight;
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] private Vector3 _startPosition;
    [SerializeField] private float projectileSpeed;
    private void Start()
    {
        _startPosition = transform.position;
    }

    new void Update()
    {
        base.Update();


        Vector3 pos = transform.position;

        Vector3 dir = (targetPosition - pos).normalized;
        pos += dir * (Time.deltaTime * projectileSpeed);
        
        pos.y = GetCurrentY();
        transform.position = pos;
        
        

    }


    private float GetStartDistance()
    {
        return (_startPosition - targetPosition).magnitude;
    }

    private float GetCurrentDistance()
    {
        return (transform.position - targetPosition).magnitude;
    }
    
    private float GetCurrentY()
    {
        if (Ascending())
        {
            return Mathf.Lerp(5,0,GetDistanceToHalfWay()/GetHalfWay());
        }
        else
        {
            return Mathf.Lerp(0,5,GetDistanceToHalfWay()/GetHalfWay());
        }
    }

    private bool Ascending()
    {
       return GetStartDistance()/2 > GetCurrentDistance();
    }

    private float GetHalfWay()
    {
        return GetStartDistance() / 2;
    }

    private float GetDistanceToHalfWay()
    {
        return Mathf.Abs(GetCurrentDistance() - GetHalfWay());
    }
    
}
