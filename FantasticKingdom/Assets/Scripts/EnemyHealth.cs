﻿using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public int startingHealth = 100;
    public int currentHealth;
    GameObject goldPile;
    public int value = 20;//temp

    AudioSource enemyAudio;
    Rigidbody rb;
    Animator anim;
    GoldPile gold;

    //CapsuleCollider capsuleCollider;
    bool isDead;
    
    // Use this for initialization
    void Awake () {
        rb = GetComponent<Rigidbody>();
        enemyAudio = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
        //capsuleCollider = GetComponent<CapsuleCollider>();
        goldPile = GameObject.Find("GoldPile");
        if (goldPile)
            gold = goldPile.GetComponent<GoldPile>();
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
        rb.AddForce(new Vector3(0, 10f, 0), ForceMode.VelocityChange);
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
        if (gold)
            gold.addGold(value);
        Destroy(gameObject, 0.5f);
    }
}
