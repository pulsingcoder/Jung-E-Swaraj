using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cameraObject;
    public float acceleration;
    public float walkAccelerationRatio;

    public float maxWalkSpeed;
    public float deaccelerate = 2;
    [HideInInspector]
    public Vector2 horizontalMovement;
    [HideInInspector]
    public float walkDeaccelerateX;
    [HideInInspector]
    public float walkDeaccelerateZ;
    [HideInInspector]
    public bool isGrounded = true;
    Rigidbody playerRigidBody;
    public float jumpVelocity = 20;
    float maxSlope = 45;

    private void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");
        horizontalMovement = new Vector2(playerRigidBody.velocity.x, playerRigidBody.velocity.z);
        if (horizontalMovement.magnitude > maxWalkSpeed)
        {
            horizontalMovement = horizontalMovement.normalized;
            horizontalMovement *= maxWalkSpeed;
        }
        playerRigidBody.velocity = new Vector3(horizontalMovement.x, playerRigidBody.velocity.y, horizontalMovement.y);
        transform.rotation = Quaternion.Euler(0, cameraObject.GetComponent<MouseLookj>().currentY, 0);

        if (isGrounded)
        {
            playerRigidBody.AddRelativeForce(inputHorizontal * acceleration, 0, inputVertical * acceleration);
        }
        else
        {
            playerRigidBody.AddRelativeForce(inputHorizontal * acceleration * walkAccelerationRatio, 0, inputVertical * walkAccelerationRatio * acceleration);

        }
        if (isGrounded)
        {
            float xMove = Mathf.SmoothDamp(playerRigidBody.velocity.x, 0, ref walkDeaccelerateX, deaccelerate);
            float zMove = Mathf.SmoothDamp(playerRigidBody.velocity.z, 0, ref walkDeaccelerateZ, deaccelerate);
            playerRigidBody.velocity = new Vector3(xMove, playerRigidBody.velocity.y, zMove);
        }

    }
}
