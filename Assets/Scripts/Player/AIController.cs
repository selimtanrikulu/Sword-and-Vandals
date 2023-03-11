using UnityEngine;

public class AIController : ControllerBase
{
    [SerializeField] private float decisionDelay;

    private float _decisionDelayCounter;
    public void GetInputs()
    {
        
    }
    private new void Update()
    {
        base.Update();

        if (_decisionDelayCounter < 0)
        {
            Decide();
            _decisionDelayCounter = decisionDelay;
        }
        else
        {
            _decisionDelayCounter -= Time.deltaTime;
        }
        
    }

    private void Decide()
    {
        if (GetDistanceToPlayer() > 1)
        {
            Vertical = 0.5f;
            Attack1Input = false;
        }
        else
        {
            Vertical = 0;
            Attack1Input = true;
        }
    }

    private float GetDistanceToPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 enemyPos = Enemy.transform.position;
        return (pos - enemyPos).magnitude;
    }
    
}
