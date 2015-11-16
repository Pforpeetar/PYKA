using UnityEngine;
using System.Collections;

public class EnemyTest : Enemy {

	public float collisionDamage = 10;
	public float knockback = 5000;
	public float stutterFactor = 500;
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

	void Chasing ()
	{
		float Xdif = target.position.x - transform.position.x;
		float Ydif = target.position.y - transform.position.y;
		Vector2 Playerdirection;
		Vector3 playerTransform; 

		Playerdirection = new Vector2 (Xdif, Ydif);
		r.velocity = (Playerdirection.normalized * movementSpeed);
		r.AddForce(new Vector2(Random.Range(-stutterFactor, stutterFactor), Random.Range(-stutterFactor, stutterFactor)));
	}

	public override void EnemyMovement ()
	{
		/*
		int maxSpeed = 5;
		r.AddForce ((target.transform.position - transform.position) * movementSpeed);
		
		if (r.velocity.x > maxSpeed) {
			r.velocity = new Vector3(maxSpeed, r.velocity.y, 0);
		}
		if (r.velocity.y > maxSpeed) {
			r.velocity = new Vector3(r.velocity.x, 5, 0);
		}
		if (r.velocity.x < -maxSpeed) {
			r.velocity = new Vector3(-maxSpeed, r.velocity.y, 0);
		}
		if (r.velocity.y < -maxSpeed) {
			r.velocity = new Vector3(r.velocity.y, -maxSpeed, 0);
		}*/
		if (target != null) {
			Chasing ();
		} else {
			r.velocity = new Vector2(0, 0);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.CompareTag ("Player")) {
			coll.gameObject.GetComponent<Player>().health -= collisionDamage;
			float verticalPush = coll.gameObject.transform.position.y - transform.position.y;
			float horizontalPush = coll.gameObject.transform.position.x - transform.position.x;

			coll.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(horizontalPush, verticalPush) * knockback);
			GetComponent<Rigidbody2D>().AddForce(new Vector2(-horizontalPush, -verticalPush) * knockback);
		}
	}
}
