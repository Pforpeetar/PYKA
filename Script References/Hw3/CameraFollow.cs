using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Rigidbody2D target;
	private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 1, transform.position.z);
	}
}
