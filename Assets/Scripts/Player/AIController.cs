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
        if (GetDistanceToPlayer() > 2)
        {
            Vertical = 0.5f;
            BasicAttackInput = false;
        }
        else
        {
            Vertical = 0;
            if (enemy.MovementState == MovementState.Died)
            {
                BasicAttackInput = false;
            }
            else
            {
                BasicAttackInput = true;
            }
        }
    }

    private float GetDistanceToPlayer()
    {
        Vector3 pos = transform.position;
        Vector3 enemyPos = enemy.transform.position;
        return (pos - enemyPos).magnitude;
    }
    
}
