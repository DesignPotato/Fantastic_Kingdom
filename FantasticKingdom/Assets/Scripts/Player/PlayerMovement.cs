using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
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

	// Health fields
	//public Image HPBar;
	//public float currentHP = 87;
	//public float maxHP = 120;
    
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
            Attack ();
            //Health ();
    }

	/** Checks for cooldown. If no cooldown and an attack button is being pressed then perform the attack */
	void Attack(){
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

		// Attack
		if (Input.GetMouseButtonDown(0) || Input.GetAxis("JoyAttack") < -0.5)
		{
			ActionAnim(Action.LIGHT_ATTACK);
			maxCD = LIGHT_ATK_CD;
			currentCD = LIGHT_ATK_CD;
		}
		if(Input.GetMouseButtonDown(1) || Input.GetAxis("JoyAttack") > 0.5)
		{
			ActionAnim (Action.HEAVY_ATTACK);
			maxCD = HEAVY_ATK_CD;
			currentCD = HEAVY_ATK_CD;
		}
	}

	/** Checks for changes in currentHP field and applies them to the UI based on maxHP */
    /*
	void Health(){
		// Checks
		if (currentHP < 0)
			currentHP = 0;
		if (currentHP > maxHP)
			currentHP = maxHP;
		// Update HPBar
		float HPRatio = 200 * currentHP / maxHP;
        if (HPBar != null)
        {
            HPBar.rectTransform.sizeDelta = new Vector2(HPRatio, 22);
            HPBar.rectTransform.localPosition = new Vector3(HPRatio / 2 - 100, 0, 0);
        }
	}
    */

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
		else if(action == Action.LIGHT_ATTACK)
		{
			anim.SetTrigger ("LightAttack");
		}
        else if (action == Action.ROLL)
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
