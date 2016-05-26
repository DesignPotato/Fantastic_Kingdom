using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

    //Mouse control character look
    public float horizontalSensitivity = 250f;

    //Movement stats
    public float speed = 6f;
    public float gravity = 3f;
    public float jumpVelocity = 6f;
    public float rollVelocity = 150f;
    public float defaultSpeed = 6f;
    public float sprintSpeed = 24f;

	// Cooldown fields
	public Image CDBar;
	private float currentCD = 0;
	private float maxCD = 1;
	private float LIGHT_ATK_CD = 35;
	private float HEAVY_ATK_CD = 60;
    float ROLL_CD = 50;
    bool attacking = false;

    //Character look
    float HorizontalDirection = 0f;
    bool dirLocked = true;

    //
    Vector3 movement;
    float distanceToGround;

    //Components
    Rigidbody playerRigidBody;
    Animator anim;
    Collider playerCollider;

    

    enum Action {JUMP, ROLL, LIGHT_ATTACK, HEAVY_ATTACK };

    void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        playerRigidBody.AddForce(Physics.gravity * gravity);
        anim = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        distanceToGround = playerCollider.bounds.extents.y;

        //Character look fields
        Vector3 angles = transform.eulerAngles;
        HorizontalDirection = angles.y;
    }

    //----------------------------------------------------------------------------||
    // Update Functions
    //----------------------------------------------------------------------------||


    //Called every physics step
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
        if(!attacking)
            Move(forward, strafe);
        Animating(forward, strafe);
     
    }

    //called every frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            playerRigidBody.velocity = new Vector3(0, jumpVelocity, 0);
        }
		if (Input.GetKeyDown(KeyCode.LeftControl) && IsGrounded())
        {
            if(currentCD == 0)
                Roll(HorizontalDirection);
        }
        Attack (HorizontalDirection);
    }

    //called last, every frame
    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            dirLocked = false;
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            dirLocked = true;
        }
        if (dirLocked)
        {
            HorizontalDirection = ThirdPersonCamera.xRotation;
            Quaternion rotation = Quaternion.Euler(0, HorizontalDirection, 0);
            transform.rotation = rotation;
        }
    }


    //----------------------------------------------------------------------------||
    // Action Functions
    //----------------------------------------------------------------------------||

    /** Checks for cooldown. If no cooldown and an attack button is being pressed then perform the attack */
    void Attack(float dir){
		// Cooldown
		if(currentCD > 0){
			float CDRatio = 50 * currentCD / maxCD;
            if (CDBar != null)
            {
                CDBar.rectTransform.sizeDelta = new Vector2(50, CDRatio);
                //CDBar.rectTransform.localPosition = new Vector3(0, 25 - CDRatio / 2, 0);
                CDBar.rectTransform.localPosition = new Vector3(-26, 51 - CDRatio / 2, 0);
            }
			currentCD--;
			return;
		}
        if (CDBar != null)
            CDBar.rectTransform.sizeDelta = new Vector2 (0, 50); // Clean up
        if (currentCD == 0)
        {
            dirLocked = true;
            attacking = false;
        }

        // Attack
        if (Input.GetMouseButtonDown(0) || Input.GetAxis("JoyAttack") < -0.5)
		{
            dirLocked = false;
            attacking = true;
            ActionAnim(Action.LIGHT_ATTACK);
			maxCD = LIGHT_ATK_CD;
			currentCD = LIGHT_ATK_CD;
        }
		if(Input.GetMouseButtonDown(1) || Input.GetAxis("JoyAttack") > 0.5)
		{   
            dirLocked = false;
            attacking = true;
            ActionAnim (Action.HEAVY_ATTACK);
			maxCD = HEAVY_ATK_CD;
			currentCD = HEAVY_ATK_CD;
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
    void Roll(float dir)
    {
        Vector3 direction;

        float forward = Input.GetAxisRaw("Vertical");
        float strafe = Input.GetAxisRaw("Horizontal");

        if (forward == 0 && strafe == 0)
        {
            direction = (transform.forward * dir);
            direction = direction.normalized * speed * Time.deltaTime;
        }
        else
        {
            direction = (transform.forward * forward) + (transform.right * strafe);
            direction = direction.normalized * speed * Time.deltaTime;
        }   
        dirLocked = false;
        attacking = true;
        maxCD = ROLL_CD;
        currentCD = ROLL_CD;
        playerRigidBody.AddForce(direction * rollVelocity, ForceMode.VelocityChange);

    }

    //Checks that player is on the ground (within error)
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distanceToGround + 0.05f);
    }


    //----------------------------------------------------------------------------||
    // Animations
    //----------------------------------------------------------------------------||

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
		else if(action == Action.LIGHT_ATTACK)
		{
			anim.SetTrigger ("LightAttack");
		}
        else if (action == Action.ROLL)
        {
            anim.SetTrigger("Roll");
        }
			
	}


}
