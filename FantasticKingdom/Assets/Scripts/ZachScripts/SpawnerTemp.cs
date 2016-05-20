using UnityEngine;
using System.Collections;

public class SpawnerTemp : MonoBehaviour {

    public GameObject goblin;
    private float time;

	// Use this for initialization
	void Start () {
        time = 3f;
	}
	
	// Update is called once per frame
	void Update () {
        time -= Time.deltaTime;
        
        if(time <= 0)
        {
            //GameObject newGoblin = (GameObject)Instantiate(Resources.Load("Goblin"), transform.position, transform.rotation);
            //newGoblin.gameObject.SetActive(true);
            Instantiate(goblin, transform.position, transform.rotation);
            time = 3f;
        }
    }
}
