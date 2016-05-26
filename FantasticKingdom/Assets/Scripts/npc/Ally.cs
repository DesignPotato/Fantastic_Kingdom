using UnityEngine;
using System.Collections;

public class Ally : Unit {

    public GameObject goldPile;
    public Collider[] potentialTargets;
    public LayerMask enemiesLayer;
    public Collider target;
    public float attackRange;

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
            target = potentialTargets[0];
        }

        if (target != null)
        {
            agent.destination = target.transform.position;
        }
	}
}
