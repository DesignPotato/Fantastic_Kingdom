using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldPile : MonoBehaviour {

    //The amount of gold the player has
    private int gold;
    //The amount of gold the player should begin with
    public int startGold = 100;
    public Text text;

    // Use this for initialization
    void Awake () {
        gold = startGold;
	}
	
	// Update is called once per frame
	void Update () {
        if(text)
            text.text = "Gold: " + gold;
    }

    public int getGold()
    {
        return gold;
    }

    public int deductGold(int amount)
    {
        int i = gold - amount >= 0 ? amount : gold;
        gold -= i;
        return i;
    }
}
