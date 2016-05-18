using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
    public float speed = 6f;
    public float gravity = 3f;
    public float jumpVelocity = 6f;
    public float rollVelocity = 150f;
    public float defaultSpeed = 6f;
    public float sprintSpeed = 24f;

    Vector3 movement;
    Rigidbody playerRigidBody;
    Animator anim;
    Collider playerCollider;
    float distanceToGround;

    enum Action {JUMP, ROLL, LIGHT_ATTACK, HEAVY_ATTACK };

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
            anim.SetFloat("RunSpeed", 2);
        }
        else
        {
            speed = defaultSpeed;
            anim.SetFloat("RunSpeed", 1);
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
        if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded())
        {      
            Roll();
        }
        if (Input.GetMouseButtonDown(0))
		{
            ActionAnim(Action.LIGHT_ATTACK);
		}
		if(Input.GetMouseButtonDown(1))
		{
			ActionAnim (Action.HEAVY_ATTACK);
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
        playerRigidBody.AddForce(new Vector3(0, jumpVelocity, 0), ForceMode.VelocityChange);
    }

    //Player carry out roll action
    void Roll()
    {
        float forward = Input.GetAxisRaw("Vertical");
        float strafe = Input.GetAxisRaw("Horizontal");
        
        Vector3 dir = (transform.forward * forward) + (transform.right * strafe);
        dir = dir.normalized * speed * Time.deltaTime;

        playerRigidBody.AddForce(dir*rollVelocity, ForceMode.VelocityChange);

    }

    //Sets animation controller states
    void Animating(float f, float s)
    {
        bool walking = f != 0f || s != 0f;
        anim.SetBool("IsWalking", walking);
    }

	void ActionAnim(Action action)
	{
		if(action == Action.HEAVY_ATTACK)
		{
			anim.SetTrigger ("HeavyAttack");
		}
		if(action == Action.LIGHT_ATTACK)
		{
			anim.SetTrigger ("LightAttack");
		}
        if (action == Action.ROLL)
        {
            anim.SetTrigger("Roll");
        }
			
	}

    //Checks that player is on the ground (within error)
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.05f);
    }
}
