using UnityEngine;
using System.Collections;

public class GlobalFlock : MonoBehaviour {

	public GameObject defaultFish;
	public GameObject[] fishPrefabs;
	public GameObject fishSchool;
	public static int tankSize = 5;

	static int numFish = 20;
	public static GameObject[] allFish = new GameObject[numFish];
	public static Vector3 goalPos = Vector3.zero;

	// Use this for initialization
	void Start () {
		for (int i = 0; i < numFish; i++) {
			Vector3 pos = new Vector3 (
				Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize)
			);
			GameObject fish = (GameObject)Instantiate (
				fishPrefabs[Random.Range (0, fishPrefabs.Length)], pos, Quaternion.identity);
			fish.transform.parent = fishSchool.transform;
			allFish [i] = fish;
		}
	}
	
	// Update is called once per frame
	void Update () {
		HandleGoalPos ();
	}

	void HandleGoalPos() {
		if (Random.Range(1, 10000) < 50) {
			goalPos = new Vector3 (
				Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize),
				Random.Range(-tankSize, tankSize)
			);
		}
	}
}
