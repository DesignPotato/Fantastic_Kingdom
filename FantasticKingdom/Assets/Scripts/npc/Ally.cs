using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Ally : Unit {

    public GameObject goldPile;
    public Collider[] PotentialTargets;
    public LayerMask enemiesLayer;
    public GameObject target;
    public float attackRangeTier2 = 20.0f;
    public float attackRangeTier3 = 30.0f;

    public GameObject Target
    {
        get { return target; }
        set {
            target = value;

            if (target != null)
                target.GetComponent<Goblin>().numberOfAttackers -= 1;

            if (value != null)
                value.GetComponent<Goblin>().numberOfAttackers += 1;

            target = value;
        }
    }
    //public GameObject originSpawner;


    //private IEnumerator<Collider> EnemiesUnderAttack {
    //    get
    //    {
    //        if (allySpawner != null && allySpawner.GetComponent<AllySpawner>() != null)
    //        {
    //            var allies = allySpawner.GetComponent<AllySpawner>().Allies;
    //            foreach (var a in allies)
    //            {
    //                var tar = a.GetComponent<Ally>().target;
    //                if (tar != null)
    //                    yield return tar.GetComponent<Collider>();
    //            }
    //        }
    //        else
    //        {
    //            yield return null;
    //        }
    //    }
    //}

    //private Transform TargetTransform
    //{
    //    get
    //    {
    //        return this.target.GetComponent<Transform>();
    //    }
    //}

    // Use this for initialization
    public override void Start () {
        Target = null;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	public override void Update () {
        if (Target == null)
        {
            Target = SeekTarget(goldPile.transform, attackRangeTier3);
            
        }

        if (Target != null)
        {
            // Give up if the target enemy runs too far out.
            var distanceFromGold = Vector3.Distance(Target.transform.position, goldPile.transform.position);
            if (distanceFromGold > attackRangeTier3)
            {
                Target = null;
                return;
            }

            // Facing the target first
            var relativePos = Target.transform.position - this.GetComponent<Transform>().position;
            var rotation = Quaternion.LookRotation(relativePos);
            var current = this.GetComponent<Transform>().rotation;
            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 2);


            agent.destination = Target.transform.position;
            agent.Resume();
        }
        else //if no target stay where it is.
        {
            agent.Stop();
        }
	}

    private GameObject SeekTarget(Transform seekSphereCentre, float seekSphereRadius)
    {
        var allTargets = Physics.OverlapSphere(seekSphereCentre.position, seekSphereRadius, enemiesLayer);
        var PotentialTargets = allTargets
            .Where(t => t.GetComponent<Goblin>().numberOfAttackers < 1)
            .Select(t => t.gameObject);

        var numOfTargetsInRange = PotentialTargets.Count();
        if (numOfTargetsInRange > 0)
        {
            var targetIndexPicked = Random.Range(0, numOfTargetsInRange);
            return PotentialTargets.ToArray()[targetIndexPicked];
        }

        return null;
    }
}
