﻿using UnityEngine;
using System.Collections;

public class Goblin : Unit {

    public const int GOLDSTEAL = 10;
    public GameObject GoldPile;
    private int goldStolen = 0;

    static float HEALTHSTAT = 10f;
    static int ARMOURSTAT = 0;
    static int MAGSTAT = 0;
    static int DAMAGESTAT = 5;

    // Use this for initialization
    public override void Start () {
        GoldPile = GameObject.Find("GoldPile");

        //Some initial values
        health = HEALTHSTAT;
        armour = ARMOURSTAT;
        magResist = MAGSTAT;
        damage = DAMAGESTAT;
        speed = 5;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.destination = GoldPile.transform.position;
    }

    public static void upStats(float modifier)
    {
        HEALTHSTAT = HEALTHSTAT * modifier;
        ARMOURSTAT = (int)(ARMOURSTAT * modifier);
        MAGSTAT = (int)(MAGSTAT * modifier);
        DAMAGESTAT = (int)(DAMAGESTAT * modifier);
    }
	
	// Update is called once per frame
	public override void Update () {
	
	}

    //Handles stealing gold
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("GoldPile") && goldStolen == 0)
        {
            goldStolen = col.gameObject.GetComponent<GoldPile>().deductGold(GOLDSTEAL);

            int escape = Random.Range(0, GameObject.Find("Spawner").GetComponent<Spawner>().spawnPointNumber());
            var spawnEscape = GameObject.Find("Spawn0" + escape);
            agent.destination = spawnEscape.transform.position;
        }

        if (col.gameObject.tag.Equals("SpawnPoint") && (goldStolen > 0 || GameObject.Find("GoldPile").GetComponent<GoldPile>().getGold() == 0))
        {
            Destroy(gameObject);
        }
    }
}
