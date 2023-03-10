using UnityEngine;


public class PlayerControl : ControllerBase
{
    
    public override void GetInputs()
    {
        Attack1Input = Input.GetKeyDown(KeyCode.Mouse0);
        Attack2Input = Input.GetKeyDown(KeyCode.Q);
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        GetStunInputTest = Input.GetKeyDown(KeyCode.RightShift);
        BreakStunInputTest = Input.GetKeyDown(KeyCode.Escape);
        BlockInput = Input.GetKey(KeyCode.Mouse1);
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        RotationInput = Input.GetAxis("Mouse X");
        RollInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    
}