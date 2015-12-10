using UnityEngine;
using System.Collections;

public class SpreadProjectile : ProjectileMovement {
	public float strayFactor = .15f;
	public float randomFactor = 0;
	public float bulletFactor = 1;
	public override void movement ()
	{

		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector

		GameObject clone = (GameObject) Instantiate(bullet, transform.position + dir, Quaternion.identity);  
		//clones prefab
		clone.GetComponent<Rigidbody2D> ().velocity = new Vector3 ((dir.x + Random.Range(-randomFactor, randomFactor)) * speed, 
		(dir.y + Random.Range(-randomFactor, randomFactor)) * speed); 
		float bulletSpreadFactor = .05f;
		for (int i = 0; i <= bulletFactor; i++) {
			createSpreadPair (strayFactor + (i * bulletSpreadFactor), randomFactor);
		}
		//Debug.Log ("Angle: " + angle);

		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone, lifeSpan);
	}

	private void createSpreadPair(float stray, float rand) {
		Vector3 sp = Camera.main.WorldToScreenPoint(transform.position); 
		//get position relative to camera
		Vector3 dir = (Input.mousePosition - sp).normalized; 
		//subtract target position and current position to get vector

		GameObject clone1 = (GameObject) Instantiate(bullet, transform.position + dir, Quaternion.identity);
		GameObject clone2 = (GameObject) Instantiate(bullet, transform.position + dir, Quaternion.identity); 
		//clones prefab
		float angle = Mathf.Atan2(dir.y, dir.x);
		clone1.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle + strayFactor + 
			Random.Range(-randomFactor, randomFactor)), speed * Mathf.Sin(angle + strayFactor + 
			Random.Range(-randomFactor, randomFactor)), 0); 
		clone2.GetComponent<Rigidbody2D> ().velocity = 
			new Vector3 (speed * Mathf.Cos(angle - strayFactor - 
			Random.Range(-randomFactor, randomFactor)), speed * Mathf.Sin(angle - strayFactor -
			Random.Range(-randomFactor, randomFactor)), 0); 
		Destroy (clone1, lifeSpan);
		Destroy (clone2, lifeSpan);
	}
}
