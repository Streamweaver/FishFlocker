using UnityEngine;
using System.Collections;

public class Fish : MonoBehaviour {

	public float speed;
	public float turnSpeed = 4.0f;
	Vector3 averageHeading;
	Vector3 averagePosition;
	float neighborDistance = 2.0f;

	// Use this for initialization
	void Start () {
		speed = Random.Range (0.5f, 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
		if (Random.Range (1, 5) < 2)
			ApplyRules ();

		transform.Translate (0, 0, Time.deltaTime * speed);
	}

	void ApplyRules() {
		GameObject[] gos;
		gos = GlobalFlock.allFish;

		Vector3 vCenter = Vector3.zero;
		Vector3 vAvoid = Vector3.zero;
		float gSpeed = 0.1f;

		Vector3 goalPos = GlobalFlock.goalPos;

		float dist;
		int groupSize = 0;

		foreach (GameObject go in gos) {
			if (go != this.gameObject) {
				dist = Vector3.Distance (go.transform.position, this.transform.position);
				if (dist <= neighborDistance) {
					vCenter += go.transform.position;
					groupSize++;

					if(dist < 1.0f) {
						vAvoid += (this.transform.position - go.transform.position);
					}

					Fish anotherFish = go.GetComponent<Fish> ();
					gSpeed += anotherFish.speed;
				}

			}
		}

		if (groupSize > 0) {
			vCenter = vCenter / groupSize + (goalPos - this.transform.position);
			speed = gSpeed / groupSize;

			Vector3 direction = (vCenter + vAvoid) - transform.position;
			if (direction != Vector3.zero) {
				transform.rotation = Quaternion.Slerp (transform.rotation,
					Quaternion.LookRotation (direction),
					turnSpeed * Time.deltaTime);
			}
		}

 	}
 }
