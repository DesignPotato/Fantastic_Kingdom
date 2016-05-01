using UnityEngine;
using System.Collections;
using System;

public class TurretDefence : MonoBehaviour {
    public GameObject myPrefab;

    private Collider[] _targets;
    private DateTime _lastShotTime;
    private int _shotsIntervalInSeconds = 3;
    private int _firePowerRadius = 25;
    private Vector3 _arrowSourcePos;

    // Use this for initialization
    void Start () {
        // Set weapon range
        var turretBaseCentre = new Vector3(transform.position.x, 0, transform.position.z);
        _targets = Physics.OverlapSphere(turretBaseCentre, _firePowerRadius);

        // Set shoot start point
        _arrowSourcePos = new Vector3(transform.position.x, 10, transform.position.z);
    }
	
	// Update is called once per frame
	void Update () {
        if (_targets.Length > 0 && _lastShotTime.AddSeconds(_shotsIntervalInSeconds) <= DateTime.Now)
        {
            _lastShotTime = DateTime.Now;
            // Shoot at the 1st target
            var myArrow = (GameObject)Instantiate(myPrefab, _arrowSourcePos, Quaternion.Euler(0, 0, 130));
            Debug.Log("Turret Fire!");
        }
	}
}
