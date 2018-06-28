using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
	public GameObject platform;
    public float beginSpawn;
    public float betweenSpawn;
	
	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves()
	{
		yield return new WaitForSeconds (beginSpawn);
		while (true) {
			var clonePos = new Vector3 (Random.Range (-2.2f, 2.2f), 6.4f, 0);
			Instantiate (platform, clonePos, Quaternion.identity);
			yield return new WaitForSeconds (betweenSpawn);
		}
	}
}
