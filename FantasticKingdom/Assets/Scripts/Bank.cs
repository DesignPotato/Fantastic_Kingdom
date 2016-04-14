using UnityEngine;
using System.Collections;

public class Bank : MonoBehaviour {
    public int GoldAmount { get; set; }

    public Bank(int initialGoldAmount)
    {
        this.GoldAmount = initialGoldAmount;
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
