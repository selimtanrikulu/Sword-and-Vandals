using System;
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
    
    
    
    
    public abstract void GetInputs();


    void Start()
    {
        CharacterController = GetComponent<CharacterController>();
        StateController = GetComponent<CharacterStateController>();
        MovementSpeed = MovementConfig.runningMovementSpeed;
    }
    
    protected virtual void Update()
    {
        GetInputs();
        Move();
        HandleRotation();
    }
    
    private void HandleRotation()
    {
        if (Mathf.Abs(RotationInput) > 0.1f)
        {
            transform.Rotate(Vector3.up, MovementConfig.rotationSpeed * Time.deltaTime * RotationInput);
        }
    }

    

    private void Move()
    {
        Vector3 moveDist = new Vector3();

        switch (StateController.MovementState)
        {
            case MovementState.Move:
                
                Transform myTransform = transform;
                Vector3 verticalMove = myTransform.forward *
                                       (Vertical * MovementConfig.runningMovementSpeed * Time.deltaTime);
                Vector3 horizontalMove = myTransform.right *
                                         (Horizontal * MovementConfig.runningMovementSpeed * Time.deltaTime);
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
            YVelocity += MovementConfig.gravity * Time.deltaTime;
        }


        moveDist += Vector3.up * (Time.deltaTime * YVelocity);

        CharacterController.Move(moveDist);
    }
    
}
