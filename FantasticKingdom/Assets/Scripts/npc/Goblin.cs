using UnityEngine;
using System.Collections;

public class Goblin : Unit {

    public GameObject GoldPile;

	// Use this for initialization
	public override void Start () {
        GoldPile = GameObject.Find("GoldPile");

        //Some initial values
        health = 10d;
        armour = 0;
        magResist = 0;
        damage = 5;
        speed = 5;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        agent.destination = GoldPile.transform.position;
    }
	
	// Update is called once per frame
	public override void Update () {
	
	}
}
