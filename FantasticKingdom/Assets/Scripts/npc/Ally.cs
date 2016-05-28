using UnityEngine;
using System.Collections;

public class Ally : Unit {

    public GameObject goldPile;
    public Collider[] potentialTargets;
    public LayerMask enemiesLayer;
    public GameObject target;
    public float attackRange;

    private Transform TargetTransform
    {
        get
        {
            return this.target.GetComponent<Transform>();
        }
    }

    // Use this for initialization
    public override void Start () {
        target = null;
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	public override void Update () {
        potentialTargets = Physics.OverlapSphere(goldPile.transform.position, attackRange, enemiesLayer);

        if (target == null && potentialTargets.Length > 0)
        {
            target = potentialTargets[0].gameObject;
        }

        if (target != null)
        {
            // Facing the target first
            var relativePos = TargetTransform.position - this.GetComponent<Transform>().position;
            var rotation = Quaternion.LookRotation(relativePos);
            var current = this.GetComponent<Transform>().rotation;
            transform.localRotation = Quaternion.Slerp(current, rotation, Time.deltaTime * 2);


            agent.destination = TargetTransform.position;
            agent.Resume();
        }
        else //if no target stay where it is.
        {
            agent.Stop();
        }
	}
}
