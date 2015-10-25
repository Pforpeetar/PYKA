using UnityEngine;
using System.Collections;


/* How to use:
 * 1. Attach script to object that will shoot.
 * 2. Create an object that represents a projectile
 * 3. Attach a rigidbody to that projectile. Reccommend making it kinematic and a trigger.
 * 4. Create a prefab of that object. Drag the prefab onto this script which is attached to the object that will shoot.
 * 5. Speed is public so you can adjust speed as need be. 
 * */
public class RangedProjectile : MonoBehaviour {
	public GameObject bullet; //bullet prefab, drag onto script
	public int speed = 10; //default projectile speed
	public float lifeSpan = 1;
	public float shotCooldown = .1f;
	private float hitTime;
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton (0) && (hitTime + shotCooldown < Time.time)) { //left click
			hitTime = Time.time;
			ShootProjectile();
		}
	}

	void ShootProjectile() {
		GameObject clone = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		//clones prefab
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector
		clone.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed, dir.y * speed, 0); 
		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone, lifeSpan);
	}
}

