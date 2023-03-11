using UnityEngine;

public class AIController : ControllerBase
{

    public override void GetInputs()
    {
        Attack1Input = true;
    }
    private new void Update()
    {
        base.Update();

    }

}
