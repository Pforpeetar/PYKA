using UnityEngine;
using System.Collections;

public class Player : Entity {
	public ProjectileMovement pM;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}
	
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
		r.velocity = new Vector2 (Input.GetAxis("Horizontal")*movementSpeed, Input.GetAxis("Vertical")*movementSpeed);

		if (Input.GetMouseButton (0) && (pM.hitTime + pM.shotCooldown < Time.time)) {
			pM.movement();
			pM.hitTime = Time.time;
		}
	}

}
