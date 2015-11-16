using UnityEngine;
using System.Collections;

public class RangeEnemy : Enemy {

	public float collisionDamage = 10;
	public float knockback = 5000;
	public GameObject refBullet;
	public float COOLDOWNTIME = 1f;
	private float timeSinceLastFired = 0f;
	private Transform playerTransform;
	private bool playerInSight = false;
	// Use this for initialization
	void Start () {
		EnemyStart ();

	}
	
	// Update is called once per frame
	void Update () {
		EnemyUpdate ();
		EnemyMovement ();
		
		Vector3 targetPos = Camera.main.WorldToScreenPoint (target.transform.position);
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		targetPos.x = targetPos.x - objectPos.x;
		targetPos.y = targetPos.y - objectPos.y;
		
		float angle = Mathf.Atan2(targetPos.y, targetPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+270));
	}

	public override void EnemyMovement ()
	{
		//Stationary
	}

	void rangedUpdate() {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		if (playerInSight == true && canFire ()) {
			//Debug.Log("stun: " + stunned);
			//Debug.Log("IN SIGHT!");
			rangedAttack ();
		}
	}
	
	private bool canFire()
	{
		if (timeSinceLastFired > COOLDOWNTIME) 
		{
			timeSinceLastFired = 0f;
			return true;
		}
		else
		{
			timeSinceLastFired += Time.deltaTime;
			return false;
		}
	}
	
	private void rangedAttack()
	{
		if (refBullet == null) return;
		GameObject clonedesu = (GameObject) Instantiate(refBullet, transform.position, Quaternion.identity);  
		projectileTrajectory (clonedesu);
		//Debug.Log(cloneVelocity);
		Physics2D.IgnoreCollision (clonedesu.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Destroy (clonedesu,2);
	}
	
	Vector3 projectileTrajectory (GameObject clone)
	{
		float Xdif = target.position.x - transform.position.x;
		float Ydif = target.position.y - transform.position.y;
		Vector2 Playerdirection;
		Vector3 playerTransform; 

		Xdif = playerTransform.x - clone.transform.position.x;
		Ydif = playerTransform.y - clone.transform.position.y;
		Playerdirection = new Vector2 (Xdif, Ydif);
		clone.GetComponent<Rigidbody2D>().velocity = (Playerdirection.normalized * 20);
		return refBullet.GetComponent<Rigidbody2D>().velocity;
	}

}
