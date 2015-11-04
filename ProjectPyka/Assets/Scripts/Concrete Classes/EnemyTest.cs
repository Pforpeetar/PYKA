using UnityEngine;
using System.Collections;

public class EnemyTest : Enemy {
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	private Vector3 playerTransform; 
	public float collisionDamage = 10;
	public float knockback = 5000;
	// Use this for initialization
	void Start () {
		EnemyStart ();
	}
	
	// Update is called once per frame
	void Update () {
		EnemyUpdate ();
		EnemyMovement ();
	}

	void Chasing ()
	{
		Xdif = target.position.x - transform.position.x;
		Ydif = target.position.y - transform.position.y;
		
		Playerdirection = new Vector2 (Xdif, Ydif);
		r.velocity = (Playerdirection.normalized * movementSpeed);
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
