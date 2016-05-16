using UnityEngine;
using System.Collections;

public class PlayerSwordAttack : MonoBehaviour {

    public int damage;
    public float timeBetweenAttacks = 0.5f;
    

    float timer;
    bool canAttack;
    // Use this for initialization
    void Awake () {
        canAttack = true;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks)
        {
            canAttack = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Damage(other.gameObject); 
        }
    }

    void Damage(GameObject enemy)
    {
        if (canAttack)
        {
            EnemyHealth eh = enemy.GetComponent<EnemyHealth>();
            eh.TakeDamage(damage);
            canAttack = false;
            timer = 0f;
        }
    }
}
