using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	private Transform target;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Utilities.getPlayerTransform()) {
			target = Utilities.getPlayerTransform();
		}
		if (!target.Equals (null)) {
			transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, transform.position.z);
		}
	}
}
