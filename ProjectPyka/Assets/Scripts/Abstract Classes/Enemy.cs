using UnityEngine;
using System.Collections;

public abstract class Enemy : Entity {
	public int level = 1;
	protected Transform target;

	// Use this for initialization
	void Start () {
		EnemyStart ();
	}

	protected void EnemyStart() {
		EntityStart ();
		if (Utilities.getPlayerTransform() != null) {
			target = Utilities.getPlayerTransform ();
		}
	}
	
	// Update is called once per frame
	void Update () {
		EnemyUpdate ();
	}

	protected void EnemyUpdate() {
		EntityUpdate ();
	}

	public abstract void EnemyMovement();
}
