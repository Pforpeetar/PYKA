using UnityEngine;
using System.Collections;

public class RangeEnemy : Enemy {

	public float collisionDamage = 10;
	public float knockback = 5000;
	public GameObject refBullet;
	public float COOLDOWNTIME = 1f;
	private float timeSinceLastFired = 0f;
	private Transform playerTransform;
	// Use this for initialization
	void Start () {
		EnemyStart ();

	}
	
	// Update is called once per frame
	void Update () {
		if (target != null) {
			EnemyUpdate ();
			EnemyMovement ();
		
			Vector3 targetPos = Camera.main.WorldToScreenPoint (target.transform.position);
		
			Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
			targetPos.x = targetPos.x - objectPos.x;
			targetPos.y = targetPos.y - objectPos.y;
		
			float angle = Mathf.Atan2 (targetPos.y, targetPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 270));
			rangedUpdate ();
		}
	}

	public override void EnemyMovement ()
	{
		//Stationary
	}

	bool findTarget() {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		Vector3 distance = playerTransform.position - transform.position;
		
		if (Mathf.Abs (distance.x) < 5  && Mathf.Abs(distance.y) < 5) {
			return true;
		}

		return false;
	}

	void rangedUpdate() {

		if (findTarget () && canFire ()) {
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
		Destroy (clonedesu,2);
	}
	
	Vector3 projectileTrajectory (GameObject clone)
	{
		Vector2 Playerdirection;
		float Xdif = playerTransform.position.x - transform.position.x;
		float Ydif = playerTransform.position.y - transform.position.y;

		Playerdirection = new Vector2 (Xdif, Ydif);
		clone.GetComponent<Rigidbody2D>().velocity = (Playerdirection.normalized * 20);
		float angle = Mathf.Atan2(playerTransform.position.y, playerTransform.position.x) * Mathf.Rad2Deg;
		clone.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90));

		return refBullet.GetComponent<Rigidbody2D>().velocity;
	}

}
