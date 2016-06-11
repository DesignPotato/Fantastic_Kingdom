using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private List<Transform> spawnPoints;
    private float spawnTimer;
    private int wave;

    //Table determining the size and composition of enemies in waves
    private int[,] spawns = new int[15, 3] { 
        {15, 1, 0 },
        {15, 3, 0 },
        {30, 7, 0 },
        {30, 0, 1, },
        {30, 3, 1 },
        {30, 1, 2 },
        {30, 5, 2 },
        {30, 5, 3 },
        {30, 10, 3 },
        {30, 10, 4 },
        {30, 10, 5 },
        {30, 15, 6 },
        {30, 0, 8 },
        {30, 10, 8 },
        {30, 10, 10 }
    };

    // Use this for initialization
    void Start () {
        spawnPoints = new List<Transform>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(g.transform);
        }
        wave = 0;
        spawnTimer = spawns[wave, 0];
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Goblin.upStats(1.1f);
            spawn();
            wave++;
            spawnTimer = spawns[wave, 0];
        }
    }

    public int spawnPointNumber()
    {
        return spawnPoints.Count;
    }

    private void spawn()
    {
        int spawnPoint = Random.Range(0, spawnPoints.Count);
        //Spawn Goblins
        for(int i = 0; i < spawns[wave, 1]; i++)
        {
            GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            newGoblin.layer = LayerMask.NameToLayer("Enemies");
            newGoblin.gameObject.SetActive(true);
        }
        //Spawn Unicorns
        for (int i = 0; i < spawns[wave, 2]; i++)
        {
            GameObject newUnicorn = (GameObject)Instantiate(Resources.Load("Unicorn"), spawnPoints[spawnPoint].position, spawnPoints[spawnPoint].rotation);
            newUnicorn.layer = LayerMask.NameToLayer("Enemies");
            newUnicorn.gameObject.SetActive(true);
        }
    }
}
