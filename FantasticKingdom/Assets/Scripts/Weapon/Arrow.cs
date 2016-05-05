﻿using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private Rigidbody rb;
    public Vector3 Direction;// = new Vector3(-5.0f, -5.0f, 0.0f);
    private float force = -30f;

	// Use this for initialization
	void Start () {
        // Set up arrow angle and force.
        var angle = transform.rotation.eulerAngles;
        Direction = new Vector3(Direction.x * force, Direction.y * force, Direction.z * force);
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Direction, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //Debug.Log("Arrow Update");
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Arrow hit");
        this.transform.position = col.contacts[0].point;
        this.GetComponent<Collider>().isTrigger = true;

        transform.parent = col.transform;

        Destroy(GetComponent<Rigidbody>());
        col.gameObject.SendMessage("arrowHit", SendMessageOptions.DontRequireReceiver);
        //GameObject.Destroy(this.gameObject);
    }
}
