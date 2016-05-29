using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private List<Transform> spawnPoints;
    public int SpawnTime;
    private float spawnTimer;
    private int wave;

    // Use this for initialization
    void Start () {
        spawnPoints = new List<Transform>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(g.transform);
        }
        spawnTimer = SpawnTime;
        wave = 0;
    }
	
	// Update is called once per frame
	void Update () {
        spawnTimer -= Time.deltaTime;

        if (spawnTimer <= 0)
        {
            Goblin.upStats(1.1f);
            spawn();
            spawnTimer = SpawnTime;
            wave++;
        }
    }

    public int spawnPointNumber()
    {
        return spawnPoints.Count;
    }

    private void spawn()
    {
        List<int> spawnNums = new List<int>();
        for(int i = 0; i < spawnPoints.Count; i++)
        {
            spawnNums.Add(i);
        }
        for(int j = 0; j < wave; j++)
        {
            int x = Random.Range(0, spawnNums.Count);
            int point = spawnNums[x];
            spawnNums.RemoveAt(x);
            for(int y = 0; y < 5; y++)
            {
                GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), spawnPoints[point].position, spawnPoints[point].rotation);
                newGoblin.layer = LayerMask.NameToLayer("Enemies");
                newGoblin.gameObject.SetActive(true);
            }
        }
    }
}
