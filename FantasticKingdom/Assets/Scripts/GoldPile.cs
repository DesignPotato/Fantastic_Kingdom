using UnityEngine;
using System.Collections;

public class GoldPile : MonoBehaviour {

    //The amount of gold the player has
    private int gold;
    //The amount of gold the player should begin with
    public int startGold = 100;

	// Use this for initialization
	void Start () {
        gold = startGold;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public int getGold()
    {
        return gold;
    }

    //Handles collisions with enemies stealing gold
    public void OnCollisionEnter(Collision col){

    }
}
