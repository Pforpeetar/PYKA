using UnityEngine;
using System.Collections;

public class SpreadProjectile : ProjectileMovement {
	public float strayFactor = 2f;
	public float Maxspread = 0.03f;
	public float Mimspread = 0.01f;
	public float Spread = 0.01f;
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
		clone1.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed, dir.y * speed); 

		if ((dir.x < 0 && dir.y > 0) || (dir.x > 0 && dir.y < 0)) {
			clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + strayFactor, dir.y * speed + strayFactor, 0); 
			clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed - strayFactor, dir.y * speed - strayFactor, 0); 
		}
		if ((dir.x > 0 && dir.y > 0) || (dir.x < 0 && dir.y < 0)) {
			clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + strayFactor, dir.y * speed - strayFactor, 0); 
			clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed - strayFactor, dir.y * speed + strayFactor, 0); 
		}

		/*float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + Mathf.Tan(angle+15), 
		                                                            dir.y * speed + Mathf.Tan(angle+15), 0); 
		clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + Mathf.Tan(angle-15), 
		                                                            dir.y * speed + Mathf.Tan(angle-15), 0); */
		//Debug.Log ("Angle: " + angle);

		/*clone1.GetComponent<Rigidbody2D> ().velocity = new Vector3 (dir.x * speed + Random.Range(-Maxspread, Maxspread), (Random.Range(-Maxspread, Maxspread) + dir.y) * speed, 0);
		clone2.GetComponent<Rigidbody2D> ().velocity = new Vector3 ((Random.Range(-Maxspread, Maxspread) + dir.x) * speed, (Random.Range(-Maxspread, Maxspread) + dir.y) * speed, 0); 
		clone3.GetComponent<Rigidbody2D> ().velocity = new Vector3 ((Random.Range(-Maxspread, Maxspread) + dir.x) * speed, (Random.Range(-Maxspread, Maxspread) + dir.y) * speed, 0);
		*/
		//Debug.Log ("Direction: " + direction);
		//Debug.Log ("Dir: " + dir);
		//set velocity of cloned object so it moves towards target along calculated vector
		Destroy (clone1, lifeSpan);
		Destroy (clone2, lifeSpan);
		Destroy (clone3, lifeSpan);
	}
}
