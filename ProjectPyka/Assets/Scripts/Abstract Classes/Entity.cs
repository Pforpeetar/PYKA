using UnityEngine;
using System.Collections;

public abstract class Entity : MonoBehaviour {
	public float health = 100;
	public float maxHealth = 100;
	public float movementSpeed = 10;
	public float maxMovementSpeed = 25;
	public float deathCounter = 0.5f;

	public SpriteRenderer healthBar; //Health bar sprite to be used
	protected Vector3 healthVector; //Vector of health bar
	protected float healthScale; //Scale health bar to size of health container.
	protected float hitTime;

	private bool deathState = false;
	protected Rigidbody2D r;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}

	protected void EntityStart() {
		r = GetComponent<Rigidbody2D> ();
		healthVector = healthBar.transform.localScale;
		healthScale = health / maxHealth;
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
		healthScale = health / maxHealth;
		healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
	}

	protected void deathCheck() {
		if (!deathState) { //check to see if they are already in death state
			if (health <= 0) {
				deathState = true;
				Destroy(gameObject, deathCounter);
				//gameObject.collider2D.enabled = false;
			}
		}
	}
}
