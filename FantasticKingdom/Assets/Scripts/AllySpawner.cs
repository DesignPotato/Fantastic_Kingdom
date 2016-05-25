using UnityEngine;
using System.Collections;

public class AllySpawner : MonoBehaviour {

    public Transform[] SpawnPoints;
    public float SpawnTime = 5.0f;
    public GameObject Ally;

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnAlly", 0, SpawnTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void SpawnAlly()
    {
        int spawnIndex = Random.Range(0, SpawnPoints.Length - 1);
        Transform spawnPoint = SpawnPoints[spawnIndex];
        Instantiate(Ally, spawnPoint.position, spawnPoint.rotation);
    }
}
