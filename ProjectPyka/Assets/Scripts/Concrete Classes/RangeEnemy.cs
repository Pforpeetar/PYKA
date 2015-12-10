using UnityEngine;
using System.Collections;

public class RangeEnemy : Enemy {

	public float collisionDamage = 10;
	public float knockback = 5000;
	public GameObject refBullet;
	public float COOLDOWNTIME = 1f;
	public float range = 7;
	private float timeSinceLastFired = 0f;
	public float stutterFactor = 500;
	private Transform playerTransform;
	public bool chaseTarget = false;
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

	void Chasing ()
	{
		float Xdif = target.position.x - transform.position.x;
		float Ydif = target.position.y - transform.position.y;
		Vector2 Playerdirection = new Vector2 (Xdif, Ydif);
		r.velocity = (Playerdirection.normalized * movementSpeed);
		r.AddForce(new Vector2(Random.Range(-stutterFactor, stutterFactor), Random.Range(-stutterFactor, stutterFactor)));
	}
	
	public override void EnemyMovement ()
	{
		if (target != null && chaseTarget) {
			Chasing ();
		} else {
			r.velocity = new Vector2(0, 0);
		}
	}

	bool findTarget() {
		playerTransform = GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		Vector3 distance = playerTransform.position - transform.position;
		
		if (Mathf.Abs (distance.x) < range  && Mathf.Abs(distance.y) < range) {
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
