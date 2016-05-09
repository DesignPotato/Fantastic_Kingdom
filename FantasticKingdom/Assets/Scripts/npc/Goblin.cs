using UnityEngine;
using System.Collections;

public class Goblin : Unit {

    public const int SPEED = 5;

	// Use this for initialization
	public override void Start () {

        //Some initial values
        health = 5d;
        armour = 0;
        magResist = 0;
        damage = 0;

        agent = GetComponent<NavMeshAgent>();
        agent.speed = SPEED;
    }
	
	// Update is called once per frame
	public override void Update () {
	
	}
}
