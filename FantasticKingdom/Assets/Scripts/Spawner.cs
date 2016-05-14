using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

    public Goblin goblinPrefab;

	// Use this for initialization
	void Start () {
	    for(int i = 0; i < 5; i++)
        {
            Goblin newGoblin = (Goblin)Instantiate(goblinPrefab, transform.position, transform.rotation);
            newGoblin.gameObject.SetActive(true);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
