using UnityEngine;
using Zenject;


public class PlayerControl : ControllerBase
{


    
    
    private new void Update()
    {
        base.Update();
        GetInputs();

    }
    public void GetInputs()
    {
        BasicAttackInput = Input.GetKeyDown(KeyCode.Mouse0);
        Skill1Input = Input.GetKeyDown(KeyCode.Q);
        Skill2Input = Input.GetKeyDown(KeyCode.E);
        Skill3Input = Input.GetKeyDown(KeyCode.R);
        JumpInput = Input.GetKeyDown(KeyCode.Space);
        GetStunInputTest = Input.GetKeyDown(KeyCode.RightShift);
        BreakStunInputTest = Input.GetKeyDown(KeyCode.Escape);
        BlockInput = Input.GetKey(KeyCode.Mouse1);
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        HorizontalRaw = Input.GetAxisRaw("Horizontal");
        VerticalRaw = Input.GetAxisRaw("Vertical");
        RotationInput = Input.GetAxis("Mouse X");
        RollInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    
}