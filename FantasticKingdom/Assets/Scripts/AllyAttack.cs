using UnityEngine;
using System.Collections;
using System;

public class AllyAttack : MonoBehaviour {
    public float attackRate = 0.5f;
    private float _nextAttack = 0.0f;

    private GameObject Target
    {
        get
        {
            return ((Ally)gameObject.GetComponentInParent(typeof(Ally))).target;
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
            Debug.Log("Attack");
        }
    }
}
