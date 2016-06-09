using UnityEngine;
using System.Collections;

public class Unicorn : Unit {

    public const int GOLDSTEAL = 25;
    public GameObject GoldPile;
    private int goldStolen = 0;

    private Animator anim;

    static float HEALTHSTAT = 25f;
    static int ARMOURSTAT = 5;
    static int MAGSTAT = 5;
    static int DAMAGESTAT = 10;

    // Use this for initialization
    public override void Start()
    {
        GoldPile = GameObject.Find("GoldPile");

        //Some initial values
        health = HEALTHSTAT;
        armour = ARMOURSTAT;
        magResist = MAGSTAT;
        damage = DAMAGESTAT;
        speed = 4;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        //agent.destination = GoldPile.transform.position;
        anim.SetBool("Attacking", true);
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
}
