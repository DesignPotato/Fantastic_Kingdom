using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth;
    public int currentHealth;

    Animator anim;
    CapsuleCollider capsuleCollider;
    bool isDead;

	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void TakeDamage(int amount)
    {
        if (isDead)
            return;

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Death();
        }
    }


    void Death()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger("Dead");
    }
}
