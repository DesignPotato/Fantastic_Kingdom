using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Ally : Unit {

    public GameObject goldPile;
    public Collider[] PotentialTargets;
    public LayerMask EnemiesLayer;
    public GameObject LocalTarget;
    public GameObject GlobalTarget;
    public float LocalTargetBreachRadius = 5.0f;
    public float LocalTargetSeekRadius = 7.0f;
    public float PatrolLimitRadius = 30.0f;

    private GameObject _activeTarget;

    Animator anim;

    //public GameObject LocalTarget // Tier 1 targets are found around the pikeman.
    //{
    //    get { return _localTarget; }
    //    set
    //    {
    //        _localTarget = value;

    //        if (_localTarget != null)
    //            _localTarget.GetComponent<Goblin>().numberOfAttackers -= 1;

    //        if (value != null)
    //            value.GetComponent<Goblin>().numberOfAttackers += 1;

    //        _localTarget = value;
    //    }
    //}

    // Use this for initialization
    public override void Awake () {
        LocalTarget = null;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        anim.SetBool("IsWalking", false);
	}
	
	// Update is called once per frame
	public override void Update () {
        if (LocalTarget == null)
        {
            LocalTarget = Barracks.SeekTarget(goldPile.transform, LocalTargetBreachRadius, LocalTargetSeekRadius, EnemiesLayer);
        }

        if (LocalTarget == null && GlobalTarget == null)
        {
            if (agent && agent.isOnNavMesh && agent.enabled)
            {
                agent.Stop();
                anim.SetBool("IsWalking", false);
            }

            return;   
        }

        // Give up if the target enemy runs too far out.
        if (LocalTarget != null && Vector3.Distance(LocalTarget.transform.position, goldPile.transform.position) > PatrolLimitRadius)
        {
            LocalTarget = null;
        }

        // Give up if the target enemy runs too far out.
        if (GlobalTarget != null && Vector3.Distance(GlobalTarget.transform.position, goldPile.transform.position) > PatrolLimitRadius)
        {
            GlobalTarget = null;
        }

        _activeTarget = LocalTarget ?? GlobalTarget;

        // No target so do nothing
        if (_activeTarget == null)
            return;
        
        // Facing the target first
        var relativePos = _activeTarget.transform.position - this.GetComponent<Transform>().position;
        var rotation = Quaternion.LookRotation(relativePos);
        var current = this.GetComponent<Transform>().rotation;
        transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 2);

        if (agent && agent.isOnNavMesh && agent.enabled)
        {
            agent.destination = _activeTarget.transform.position;
            agent.speed = (float)speed;
            agent.Resume();
            anim.SetBool("IsWalking", true);
        }
	}


}
