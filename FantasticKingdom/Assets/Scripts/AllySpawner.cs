using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllySpawner : MonoBehaviour {

    public Transform[] SpawnPoints;
    public float SpawnTime = 5.0f;
    public GameObject AllyPrefab;
    public LayerMask EnemiesLayer;
    public IList<GameObject> Allies;
    public int PopulationCap = 20;
    public float attackRangeTier2 = 20.0f;
    public float attackRangeTier3 = 30.0f;

    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnAlly", 0.1f, SpawnTime);
        Allies = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnAlly()
    {
        if (Allies.Count >= PopulationCap)
            return;

        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[spawnIndex];
        GameObject ally = (GameObject)Instantiate(AllyPrefab, spawnPoint.position, spawnPoint.rotation);
        Ally allyScript = ally.GetComponent<Ally>();
        allyScript.goldPile = GameObject.Find("GoldPile");
        allyScript.speed = 3;
        allyScript.enemiesLayer = this.EnemiesLayer;
        allyScript.attackRangeTier2 = attackRangeTier2;
        allyScript.attackRangeTier3 = attackRangeTier3;
        //allyScript.originSpawner = gameObject;
        Allies.Add(ally);
    }
}
