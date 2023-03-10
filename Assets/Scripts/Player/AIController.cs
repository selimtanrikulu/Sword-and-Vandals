using UnityEngine;

public class AIController : ControllerBase
{

    public override void GetInputs()
    {
        Horizontal = 1;
    }
    private new void Update()
    {
        base.Update();

    }

}
