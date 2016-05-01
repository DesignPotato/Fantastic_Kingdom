using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    private Rigidbody rb;
    private Vector3 direction = new Vector3(-5.0f, -5.0f, 0.0f);

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(direction, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        
        //Debug.Log("Arrow Update");
    }

    void OnCollisionEnter(Collision col)
    {
        Debug.Log("Arrow hit");
        GameObject.Destroy(this.gameObject);
    }
}
