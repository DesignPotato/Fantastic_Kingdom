using UnityEngine;
using System.Collections;
using System;

public class AllyAttack : MonoBehaviour {
    public float attackRate = 0.5f;
    public Animator anim;
    private float _nextAttack = 0.0f;
    
    void Awake()
    {
        //this script must be on child of ally
        anim = transform.parent.GetComponent<Animator>();
    }
    
    private GameObject Target
    {
        get
        {
            return ((Ally)gameObject.GetComponentInParent(typeof(Ally))).Target;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        Attack(other);
    }

    void OnTriggerStay(Collider other)
    {
        Attack(other);
    }

    private void Attack(Collider col)
    {
        if (col.gameObject == Target && Time.time > _nextAttack)
        {
            var enemyHealth = col.gameObject.GetComponent<EnemyHealth>();
            enemyHealth.TakeDamage(50);
            _nextAttack = Time.time + attackRate;
            if(anim)
                anim.SetTrigger("Attack");
            Debug.Log("Attack");
        }
    }
}
