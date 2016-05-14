using UnityEngine;
using System.Collections;

public class Arrow : MonoBehaviour {

    public Vector3 ShootDirection;// = new Vector3(-5.0f, -5.0f, 0.0f);
    public float ShootForce;// = -30f;
    
	// Use this for initialization
	void Start () {
        //Debug.Log(string.Format("{0}", transform.position.ToString()));
        // Set up arrow angle and force.
        ShootDirection = new Vector3(ShootDirection.x * ShootForce, ShootDirection.y * ShootForce, ShootDirection.z * ShootForce);
        var rb = GetComponent<Rigidbody>();
        rb.AddForce(ShootDirection, ForceMode.Impulse);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        //Debug.Log("Arrow Update");
    }

    void OnCollisionEnter(Collision col)
    {        
        //Debug.Log("Arrow hit");
        this.transform.position = col.contacts[0].point;
        this.GetComponent<Collider>().isTrigger = true;
        transform.parent = col.transform;
        Destroy(GetComponent<Rigidbody>());
        col.gameObject.SendMessage("arrowHit", SendMessageOptions.DontRequireReceiver);
        //GameObject.Destroy(this.gameObject);
    }
}
