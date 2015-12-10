using UnityEngine;
using System.Collections;

public class Orbit : MonoBehaviour {
	public Transform target;
	public float degrees = 1;
	public float radius = 1;
	public bool orbitDir = true;
	public int startPosition = 0;

	void Start() {
		if (startPosition == 0) {
			Vector3 orbit = new Vector3 (target.position.x + radius, target.position.y + radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 1) {
			Vector3 orbit = new Vector3 (target.position.x + radius, target.position.y + -radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 2) {
			Vector3 orbit = new Vector3 (target.position.x + -radius, target.position.y + radius, target.position.z);
			this.transform.position = orbit;
		} else if (startPosition == 3) {
			Vector3 orbit = new Vector3 (target.position.x + -radius, target.position.y + -radius, target.position.z);
			this.transform.position = orbit;
		}
	}

	// Update is called once per frame
	void Update () {
		//this.transform.position = target.transform.position * 2;
		if (orbitDir == true) {
			transform.RotateAround (target.transform.position, Vector3.forward, degrees);
		} else {
			transform.RotateAround (target.transform.position, Vector3.back, degrees);
		}
	}

}
