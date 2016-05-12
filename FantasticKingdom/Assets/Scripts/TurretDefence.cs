using UnityEngine;
using System.Collections;
using System;

public class TurretDefence : MonoBehaviour {
    public GameObject ArrowPrefab;
    public int FirePowerRadius = 10;
    //public float ArrowSourcePosY = 9f;
    public LayerMask Mask;

    private Collider[] _targets;
    private DateTime _lastShotTime;
    private int _shotsIntervalInSeconds = 3;
    private Vector3 _arrowSourcePos;

    // Use this for initialization
    void Start () {
        // Set shoot start point
        Debug.Log(GetComponent<Collider>().bounds.size.y);
        _arrowSourcePos = new Vector3(transform.position.x, GetComponent<Collider>().bounds.size.y + 8, transform.position.z);
        
    }
	
	// Update is called once per frame
	void Update () {
        // Set weapon range
        _targets = Physics.OverlapSphere(transform.position, FirePowerRadius, Mask);

        // Shoot in a interval
        if (_targets.Length > 0 && _lastShotTime.AddSeconds(_shotsIntervalInSeconds) <= DateTime.Now)
        {
            //_arrowSourcePos = Vector3.MoveTowards(_arrowSourcePos, _targets[0].transform.position, 0.5f);

            _arrowSourcePos = Vector3.MoveTowards(_arrowSourcePos, _targets[0].transform.position, 3);
            // Shoot at the 1st target
            var myArrow = (GameObject)Instantiate(ArrowPrefab, _arrowSourcePos, Quaternion.identity);
            myArrow.transform.LookAt(_targets[0].transform.position);
            myArrow.transform.Rotate(90, 0, 0);
            myArrow.GetComponent<Arrow>().Direction = (myArrow.transform.position - _targets[0].transform.position).normalized;
            myArrow.GetComponent<Arrow>().ArrowSource = this.gameObject;
            _lastShotTime = DateTime.Now;
            Debug.Log("Turret Fire!");
        }
	}
}
