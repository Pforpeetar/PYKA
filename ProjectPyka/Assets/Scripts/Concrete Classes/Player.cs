using UnityEngine;
using System.Collections;

public class Player : Entity {
	public WeaponHandler wH;

	private int weaponIndex = 0;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}
	
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
		r.velocity = new Vector2 (Input.GetAxis("Horizontal")*movementSpeed, Input.GetAxis("Vertical")*movementSpeed);

		if (Input.GetMouseButton (0) && (wH.pM[weaponIndex].hitTime + wH.pM[weaponIndex].shotCooldown < Time.time)) {
			wH.createProjectile(weaponIndex);
			//pM.hitTime = Time.time;
		}

		if (Input.GetMouseButtonDown (1)) {
			weaponIndex++;
			if (weaponIndex >= wH.pM.Count) {
				weaponIndex = 0;
			}
		}
		Vector3 mousePos = Input.mousePosition;

		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
	}

}
