using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    private List<Transform> spawnPoints;

	// Use this for initialization
	void Start () {
        spawnPoints = new List<Transform>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("SpawnPoint"))
        {
            spawnPoints.Add(g.transform);
            GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), g.transform.position, g.transform.rotation);
            newGoblin.gameObject.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        if(false)
        {
            GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), transform.position, transform.rotation);
            newGoblin.gameObject.SetActive(true);
        }
    }
}
