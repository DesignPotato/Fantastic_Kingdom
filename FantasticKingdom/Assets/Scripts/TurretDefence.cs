using UnityEngine;
using System.Collections;
using System;

public class TurretDefence : MonoBehaviour {
    public GameObject ArrowPrefab;
    public int FirePowerRadius = 10;
    public LayerMask Mask;

    private Collider[] _targets;
    private DateTime _lastShotTime;
    private int _shotsIntervalInSeconds = 3;
    private Vector3 _arrowSourcePos;

    // Use this for initialization
    void Start () {
        // Set shoot start point
        //Debug.Log(String.Format("Arrow Source x: {0}; y: {1}; z: {2}", GetComponent<Collider>().bounds.size.x, GetComponent<Collider>().bounds.size.y, GetComponent<Collider>().bounds.size.z));
        _arrowSourcePos = new Vector3(transform.position.x, GetComponent<Collider>().bounds.size.y, transform.position.z);
        
    }
	
	// Update is called once per frame
	void Update () {
        // Set weapon range
        _targets = Physics.OverlapSphere(transform.position, FirePowerRadius, Mask);

        // Shoot in a interval
        if (_targets.Length > 0 && _lastShotTime.AddSeconds(_shotsIntervalInSeconds) <= DateTime.Now)
        {
            //If shotting from middle of the tower, the arrow will hit inside of the tower wall.
            //Adjust where the arrow will originally shoot from to outside of the tower wall.
            var firstTargetPos = _targets[0].transform.position;
            var arrowSourcePosOffset = new Vector3(firstTargetPos.x, _arrowSourcePos.y, firstTargetPos.z);
            var adjustedArrowSourcePos = Vector3.MoveTowards(_arrowSourcePos, arrowSourcePosOffset, 1.7f); // 1.7f is the distance required to move the arrow outside the tower wall.
            //Debug.Log(String.Format("Adjusted arrow Source x: {0}; y: {1}; z: {2}", adjustedArrowSourcePos.x, adjustedArrowSourcePos.y, adjustedArrowSourcePos.z));
            // Shoot at the 1st target
            var myArrow = (GameObject)Instantiate(ArrowPrefab, adjustedArrowSourcePos, Quaternion.identity);
            myArrow.transform.LookAt(_targets[0].transform.position);
            myArrow.transform.Rotate(90, 0, 0);
            myArrow.GetComponent<Arrow>().ShootDirection = (myArrow.transform.position - _targets[0].transform.position).normalized;
            myArrow.GetComponent<Arrow>().ShootForce = -30f; //this.gameObject;
            _lastShotTime = DateTime.Now;
            //Debug.Log("Turret Fire!");
        }
	}
}
