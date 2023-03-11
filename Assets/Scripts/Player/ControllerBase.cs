using System.Linq;
using UnityEngine;

public abstract class ControllerBase : MonoBehaviour
{
    protected CharacterController CharacterController;
    protected CharacterStateController StateController;

    public float MovementSpeed { get; set; }

    public float YVelocity { get; set; }

    #region Inputs
    public float Horizontal { get; set; }
    public float Vertical { get; set; }
    public bool Attack1Input { get; set; }
    public bool Attack2Input { get; set; }
    public bool BlockInput { get; set; }
    public bool RollInput { get; set; }
    public bool GetStunInputTest { get; set; }
    public bool BreakStunInputTest { get; set; }
    public bool JumpInput { get; set; }
    public float RotationInput { get; set; }

    #endregion
    
 


    protected ControllerBase Enemy;
    private float _userRotation;

    private void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        StateController = GetComponent<CharacterStateController>();
        MovementSpeed = MovementConfig.RunningMovementSpeed;
        
        Enemy = FindObjectsOfType<ControllerBase>().FirstOrDefault(x=>x != this);
    }
    
    protected virtual void Update()
    {
        Move();
        HandleRotation();
    }
    
    private void HandleRotation()
    {
        Vector3 lookAt = (Enemy.transform.position - transform.position);
        lookAt.y = 0;
        lookAt.Normalize();
        float rotY = Mathf.Atan2(lookAt.x, lookAt.z) * Mathf.Rad2Deg;
        transform.eulerAngles = new Vector3(0, rotY + _userRotation, 0);
    }

    

    private void Move()
    {
        Vector3 moveDist = new Vector3();

        switch (StateController.MovementState)
        {
            case MovementState.Move:
                
                Transform myTransform = transform;
                Vector3 verticalMove = myTransform.forward *
                                       (Vertical * MovementConfig.RunningMovementSpeed * Time.deltaTime);
                Vector3 horizontalMove = myTransform.right *
                                         (Horizontal * MovementConfig.RunningMovementSpeed * Time.deltaTime);
                moveDist = verticalMove + horizontalMove;
                break;

            case MovementState.RollForward:
                moveDist = transform.forward * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollBackward:
                moveDist = -transform.forward * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollRight:
                moveDist = transform.right * (MovementSpeed * Time.deltaTime);
                break;

            case MovementState.RollLeft:
                moveDist = -transform.right * (MovementSpeed * Time.deltaTime);
                break;


            case MovementState.Stunned:
                //nothing
                break;
        }

        //Apply gravity
        if (!CharacterController.isGrounded)
        {
            YVelocity += MovementConfig.Gravity * Time.deltaTime;
        }


        moveDist += Vector3.up * (Time.deltaTime * YVelocity);

        CharacterController.Move(moveDist);
    }
    
}
