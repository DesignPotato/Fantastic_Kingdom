﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GoldPile : MonoBehaviour {


    //The amount of gold the player should begin with
    public int startGold = 101;
    public Text text;
    //The amount of gold the player has
	private int gold;
    int maxGold;
    int kills = 0;
    //Interest
    public float interestRate = 0.01f; // 1% every 30 sec
    public float interestPeriod = 5.0f;// In seconds
    private float _nextInterestDue;

    // Use this for initialization
    void Awake () {
		gold = startGold;
        maxGold = gold;
        _nextInterestDue = Time.time + interestPeriod;
	}
	
	// Update is called once per frame
	void Update () {
        if (_nextInterestDue < Time.time)
        {
            addGold((int)(gold * interestRate));
            _nextInterestDue += interestPeriod;
        }

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
