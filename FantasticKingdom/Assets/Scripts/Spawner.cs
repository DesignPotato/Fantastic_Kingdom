using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private List<Transform> spawnPoints;
    private float spawnTimer;
    private int wave;

    private int[,] spawns = new int[15, 2] { 
        {30, 1 },
        {15, 3 },
        {30, 7 },
        {30, 0 },
        {30, 3 },
        {30, 1 },
        {30, 5 },
        {30, 5 },
        {30, 10 },
        {30, 10 },
        {30, 10 },
        {30, 15 },
        {30, 0 },
        {30, 10 },
        {30, 10 }
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


        //List<int> spawnNums = new List<int>();
        //for(int i = 0; i < spawnPoints.Count; i++)
        //{
        //   spawnNums.Add(i);
        //}
        //for(int j = 0; j < wave; j++)
        //{
        //  int x = Random.Range(0, spawnNums.Count);
        // int point = spawnNums[x];
        //spawnNums.RemoveAt(x);
        //for(int y = 0; y < 5; y++)
        //{
        //   GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), spawnPoints[point].position, spawnPoints[point].rotation);
        //  newGoblin.layer = LayerMask.NameToLayer("Enemies");
        //  newGoblin.gameObject.SetActive(true);
        //}
        // }
    }
}
