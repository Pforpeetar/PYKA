using UnityEngine;
using System.Collections;

public class SpreadProjectile : ProjectileMovement {
	public float strayFactor = .15f;
	public float randomFactor = 0;
	public override void movement ()
	{
		GameObject clone1 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		GameObject clone2 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		GameObject clone3 = (GameObject) Instantiate(bullet, transform.position, Quaternion.identity); 
		//clones prefab
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector

		float angle = Mathf.Atan2(dir.y, dir.x);

		clone1.GetComponent<Rigidbody2D> ().velocity = new Vector3 ((dir.x + Random.Range(-randomFactor, randomFactor)) * speed, 
		                                                            (dir.y + Random.Range(-randomFactor, randomFactor)) * speed); 
		clone2.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle + strayFactor + 
			Random.Range(-randomFactor, randomFactor)), speed * Mathf.Sin(angle + strayFactor + 
			Random.Range(-randomFactor, randomFactor)), 0); 
		clone3.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle - strayFactor - 
			Random.Range(-randomFactor, randomFactor)), speed * Mathf.Sin(angle - strayFactor -
			Random.Range(-randomFactor, randomFactor)), 0); 
		//Debug.Log ("Angle: " + angle);

		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone1, lifeSpan);
		Destroy (clone2, lifeSpan);
		Destroy (clone3, lifeSpan);
	}
}
