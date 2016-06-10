using UnityEngine;
using System.Collections;

public class GoblinTemp : Unit {

    public const int GOLDSTEAL = 10;
    GameObject GoldPile;
    GameObject Spawner;
    private int goldStolen = 0;

	// Use this for initialization
	public override void Awake () {
        GoldPile = GameObject.FindGameObjectWithTag("GoldPile");
        Spawner = GameObject.FindGameObjectWithTag("Spawner");
        //GoldPile = GameObject.Find("GoldPile");

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

    //Handles stealing gold
    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject == GoldPile && goldStolen == 0)
        {
            goldStolen = col.gameObject.GetComponent<GoldPileTemp>().deductGold(GOLDSTEAL);
            agent.destination =Spawner.transform.position;
        }

        if (col.gameObject == Spawner && (goldStolen > 0 || GoldPile.GetComponent<GoldPileTemp>().getGold() == 0))
        {
            Destroy(gameObject);
        }
    }
}
