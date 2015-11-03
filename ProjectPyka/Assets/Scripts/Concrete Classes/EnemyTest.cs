using UnityEngine;
using System.Collections;

public class EnemyTest : Enemy {
	private Vector2 Playerdirection;
	private float Xdif;
	private float Ydif;
	private Vector3 playerTransform; 

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
		Chasing ();
	}
}
