using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;

    AudioSource enemyAudio;
    Rigidbody rb;

    //CapsuleCollider capsuleCollider;
    bool isDead;
    
    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        enemyAudio = GetComponent<AudioSource>();
        //capsuleCollider = GetComponent<CapsuleCollider>();

        currentHealth = startingHealth;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        enemyAudio.Play();
        currentHealth -= amount;
        rb.AddForce(new Vector3(0, 100f, 0), ForceMode.VelocityChange);
        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        //capsuleCollider.isTrigger = true;
        Destroy(gameObject, 0.5f);
    }
}
