using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldPile : MonoBehaviour {


    //The amount of gold the player should begin with
    public int startGold = 100;
    public Text text;
    //The amount of gold the player has
    private int gold;
    int maxGold;
    int kills = 0;

    // Use this for initialization
    void Awake () {
        gold = startGold;
        maxGold = gold;
	}
	
	// Update is called once per frame
	void Update () {
        if(text)
            text.text = "" + gold;
    }

    public int getGold()
    {
        return gold;
    }

    public int getMaxGold()
    {
        return maxGold;
    }

    public int deductGold(int amount)
    {
        int i = gold - amount >= 0 ? amount : gold;
        gold -= i;
        return i;
    }

    public void addGold(int amount)
    {
        gold += amount;
        if (gold > maxGold)
        {
            maxGold = gold;
        }
    }

    public void addKill()
    {
        kills += 1;
    }

    public int getKills()
    {
        return kills;
    }

}
