using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Barracks : MonoBehaviour {
	
	public GameObject PikemanPrefab;
    public float SpawnTime = 5.0f;
    public IList<GameObject> Allies = new List<GameObject>();
    public int PopulationCap = 7;
    public LayerMask EnemiesLayer = 1 << 8;
    public float attackRangeTier2 = 20.0f;
    public float attackRangeTier3 = 30.0f;

    private Vector3 SpawnPoint;

    // Use this for initialization
    void Start () {
		InvokeRepeating("SpawnUnit", 0.1f, SpawnTime);

        // Get spawn position
        SpawnPoint = transform.position + new Vector3(0, 1, 2);
	}

	void SpawnUnit(){
		if (Allies.Count >= PopulationCap)
			return;
		GameObject ally = (GameObject)Instantiate(PikemanPrefab, SpawnPoint, this.transform.rotation);
		ally.transform.Translate (new Vector3 (0 , 0, 4));
		Ally allyScript = ally.GetComponent<Ally>();
		allyScript.goldPile = GameObject.Find("GoldPile");
		allyScript.speed = 3;
		allyScript.enemiesLayer = EnemiesLayer;
		allyScript.attackRangeTier2 = attackRangeTier2;
		allyScript.attackRangeTier3 = attackRangeTier3;
		Allies.Add(ally);
	}
}
