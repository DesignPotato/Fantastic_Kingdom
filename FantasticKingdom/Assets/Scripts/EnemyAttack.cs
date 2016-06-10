using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {

    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    GameObject player;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    Animator anim;
    bool playerInRange;
    float timer;


    void Awake () {
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent<Animator>();
        //Debug.Log("player: " + player + " health: " + playerHealth);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player in range");
            playerInRange = true;
            player = other.gameObject;
            playerHealth = player.GetComponent<PlayerHealth>();
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("player not in range");
            playerInRange = false;
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenAttacks && playerInRange && enemyHealth.currentHealth > 0)
        {
            Attack();
        }
        /*
        if (playerHealth.currentHealth <= 0)
        {

        }
        */
    }


    void Attack()
    {
        Debug.Log("attacking player");
        timer = 0f;

        if (playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
        if (anim)
            anim.SetTrigger("Attacking");
    }
}
