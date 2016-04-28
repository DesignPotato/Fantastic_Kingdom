using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;
    public float gravity = 3f;
    public float jumpVelocity = 6f;
    public float defaultSpeed = 6f;
    public float sprintSpeed = 24f;

    Vector3 movement;
    Rigidbody playerRigidBody;
    Animator anim;
    Collider playerCollider;
    float distanceToGround;

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.AddForce(Physics.gravity * gravity);
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        distanceToGround = playerCollider.bounds.extents.y;
    }

	void FixedUpdate()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float strafe = Input.GetAxisRaw("Horizontal");

        if (Input.GetKey("left shift"))
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = defaultSpeed;
        }

        Move(forward, strafe);
        Animating(forward, strafe);
        
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRigidBody.velocity = new Vector3(0, jumpVelocity, 0);
        }
    }

    //control player movement
    void Move(float forward, float right)
    {
        movement = (transform.forward * forward) + (transform.right * right);
        movement = movement.normalized * speed * Time.deltaTime;
        playerRigidBody.MovePosition(transform.position + movement);
    }

    //player carry out jump action
    void Jump()
    {
            playerRigidBody.velocity = new Vector3(0, jumpVelocity, 0);  
    }

    //Sets animation controller states
    void Animating(float f, float s)
    {
        bool walking = f != 0f || s != 0f;
        anim.SetBool("IsWalking", walking);
        //TODO set animations for attack/jumping/rolling etc
    }

    //Checks that player is on the ground (within error)
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.4f);
    }
}
