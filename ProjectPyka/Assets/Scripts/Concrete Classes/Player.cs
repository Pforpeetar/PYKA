using UnityEngine;
using System.Collections;

public class Player : Entity {
	public WeaponHandler wH;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}
	
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
		rigidBody.velocity = new Vector2 (Input.GetAxis("Horizontal")*movementSpeed, Input.GetAxis("Vertical")*movementSpeed);

		if (Input.GetMouseButton (0) && (wH.pM.hitTime + wH.pM.shotCooldown < Time.time)) {
			wH.createProjectile();
		}
	}
}
