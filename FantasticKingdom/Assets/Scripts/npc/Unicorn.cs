using UnityEngine;
using System.Collections;

public class Unicorn : Unit {

    public const int GOLDSTEAL = 25;
    public GameObject GoldPile;
    private int goldStolen = 0;
    private Animator anim;
    private GameObject target;
    private float attackTime = 1f;
    private float nextAttack = 0f;

    public LayerMask AlliesLayer;
    public LayerMask AlliedBuildings;
    public LayerMask Spawns;
    //public float LocalTargetBreachRadius = 5.0f;
    public float SeekRadius = 7.0f;

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
        speed = 6;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = null;
    }

    public static void upStats(float modifier)
    {
        HEALTHSTAT = HEALTHSTAT * modifier;
        ARMOURSTAT = (int)(ARMOURSTAT * modifier);
        MAGSTAT = (int)(MAGSTAT * modifier);
        DAMAGESTAT = (int)(DAMAGESTAT * modifier);
    }

    private GameObject seekTarget(float radius, LayerMask mask)
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radius, mask);
        if (targets.Length == 0)
        {
            //No enemy in range
            return null;
        }
        //Return random unit within range
        int targetId = Random.Range(0, targets.Length);
        return targets[targetId].gameObject;
    }

    // Update is called once per frame
    public override void Update()
    {
        //If dont have a target, check for new one
        if (target == null)
        {
            //First seek nearest allied unit
            target = seekTarget(SeekRadius, AlliesLayer);

            if (target == null)
            {
                //No enemy in range, check for buildings instead
                target = seekTarget(SeekRadius, AlliedBuildings);
            }

            if (target == null)
            {
                //No enemies or buildings in range, go for gold instead
                target = GoldPile;
            }
        }
        //Reset destination
        agent.destination = target.transform.position;

        //If at spawn point, exit the battle
        Collider[] targets = Physics.OverlapSphere(transform.position, 3f, Spawns);
        if (targets.Length > 0 && (goldStolen > 0 || GameObject.Find("GoldPile").GetComponent<GoldPile>().getGold() == 0))
        {
            Destroy(gameObject);
        }
    }

    //Handles stealing gold
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name.Equals("GoldPile") && goldStolen == 0)
        {
            goldStolen = col.gameObject.GetComponent<GoldPile>().deductGold(GOLDSTEAL);
            //Choose escape point
            int escape = Random.Range(1, GameObject.Find("Spawner").GetComponent<Spawner>().spawnPointNumber());
            GameObject spawnEscape;
            if (escape > 9)
            {
                spawnEscape = GameObject.Find("Spawn" + escape);
            }
            else
            {
                spawnEscape = GameObject.Find("Spawn0" + escape);
            }
            target = spawnEscape;
        }

        //If colliding with target, attack
        if (col.gameObject.Equals(target) && Time.time > nextAttack)
        {
            attack(col);
        }
    }

    //For attacking
    private void attack(Collision col)
    {
        Debug.Log("Attacking");
        HealthScriptAbstract health;
        if (col.gameObject.tag.Equals("Player"))
        {
            health = col.gameObject.GetComponent<PlayerHealth>();
        }
        else
        {
            health = col.gameObject.GetComponent<AllyHealth>();
        }
        health.takeDamage(damage);
        nextAttack = Time.time + attackTime;
        if (anim)
        {
            anim.SetTrigger("Attacking");
        }
    }
}
