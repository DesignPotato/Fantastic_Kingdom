using UnityEngine;
using System.Collections;

public class AllyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    AudioSource healthAudio;
    Rigidbody rb;
    Animator anim;
    NavMeshAgent agent;

    bool isDead;

    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        healthAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        if (healthAudio)
            healthAudio.Play();
        currentHealth -= amount;
        if (rb && agent)
        {
            //agent.enabled = false;
            rb.drag = 1;
            rb.AddForce(new Vector3(0, 6f, 0), ForceMode.VelocityChange);
        }
        if (currentHealth <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        if (anim)
            anim.SetTrigger("Die");
        //capsuleCollider.isTrigger = true;
        if (agent)
            agent.enabled = false;
        Destroy(gameObject, 2.5f);
    }

    void OnCollisionEnter(Collision other)
    {
        if (agent.enabled == false)
        {
            if (other.gameObject.tag == "Ground")
            {
                rb.drag = Mathf.Infinity;
                agent.enabled = true;
                Debug.Log("enabled agent");
            }
        }

    }
}
