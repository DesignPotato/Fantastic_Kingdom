using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllySpawner : MonoBehaviour {

	public static float SpawnTime = 5.0f;
	public static IList<GameObject> Allies = new List<GameObject>();
	public static int PopulationCap = 20;
	public static LayerMask EnemiesLayer = 1 << 8;
	public static float attackRangeTier2 = 20.0f;
	public static float attackRangeTier3 = 30.0f;

}
