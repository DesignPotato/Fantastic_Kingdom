using UnityEngine;
using System.Collections;

public class AllySpawner : MonoBehaviour {

    public Transform[] SpawnPoints;
    public float SpawnTime = 5.0f;
    public GameObject Ally;
    public LayerMask EnemiesLayer;

    // Use this for initialization
    void Start () {
        InvokeRepeating("SpawnAlly", 0.1f, SpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnAlly()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length);
        Transform spawnPoint = SpawnPoints[spawnIndex];
        GameObject ally = (GameObject)Instantiate(Ally, spawnPoint.position, spawnPoint.rotation);
        Ally allyScript = ally.GetComponent<Ally>();
        allyScript.goldPile = GameObject.Find("GoldPile");
        allyScript.speed = 3;
        allyScript.enemiesLayer = this.EnemiesLayer;
        allyScript.attackRange = 10.0f;
    }
}
