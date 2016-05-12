using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {

    public Transform followTarget;
    public Animator anim;

    NavMeshAgent nma;


	void Awake () {
        nma = GetComponent<NavMeshAgent>();
        
	}
	
	// Update is called once per frame
	void Update () {
        Follow();
    }

    void Follow()
    {
        anim.SetBool("IsWalking", true);
        nma.destination = followTarget.position;
    }
}
