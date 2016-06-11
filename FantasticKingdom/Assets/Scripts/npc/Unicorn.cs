using UnityEngine;
using System.Collections;

public class Unicorn : Unit {

    public const int GOLDSTEAL = 25;
    public GameObject GoldPile;
    private int goldStolen = 0;
    private Animator anim;
    private GameObject target;

    public LayerMask AlliesLayer;
    public LayerMask AlliedBuildings;
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
        speed = 4;

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        target = null;

        anim.SetBool("IsWalking", false);
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
            agent.destination = target.transform.position;
        }



        // Facing the target first
        //var relativePos = _activeTarget.transform.position - this.GetComponent<Transform>().position;
        //var rotation = Quaternion.LookRotation(relativePos);
        //var current = this.GetComponent<Transform>().rotation;
        //transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 2);

        //if (agent && agent.isOnNavMesh && agent.enabled)
       // {
         //   anim.SetBool("IsWalking", true);
           // agent.destination = _activeTarget.transform.position;
           // agent.speed = (float)speed;
           // agent.Resume();
       // }
    }
}
