using UnityEngine;
using System.Collections;

public class Goblin : Unit {

    public const int GOLDSTEAL = 10;
    public GameObject GoldPile;
    private int goldStolen = 0;

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

    //Handles stealing gold
    public void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.name.Equals("GoldPile") && goldStolen == 0)
        {
            goldStolen = col.gameObject.GetComponent<GoldPile>().deductGold(GOLDSTEAL);

            var spawnerObj = GameObject.Find("Spawner");
            if (spawnerObj != null)
                agent.destination = spawnerObj.transform.position;
        }

        if (col.gameObject.name.Equals("Spawner") && (goldStolen > 0 || GameObject.Find("GoldPile").GetComponent<GoldPile>().getGold() == 0))
        {
            Destroy(gameObject);
        }
    }
}
