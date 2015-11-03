using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	public float health = 100;
	public float maxHealth = 100;
	public float movementSpeed = 10;
	private bool deathState = false;
	protected Rigidbody2D r;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}

	protected void EntityStart() {
		r = GetComponent<Rigidbody2D> ();
	}
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
	}

	protected void EntityUpdate() {
		HealthUpdate ();
		deathCheck ();
	}

	protected void HealthUpdate() {
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			health = 0;
		}
	}

	protected void deathCheck() {
		if (!deathState) { //check to see if they are already in death state
			if (health <= 0) {
				deathState = true;
				Destroy(gameObject, 1);
				//gameObject.collider2D.enabled = false;
			}
		}
	}
}
