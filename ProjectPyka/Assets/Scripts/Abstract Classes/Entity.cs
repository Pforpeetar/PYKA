﻿using UnityEngine;
using System.Collections;

public class Entity : MonoBehaviour {
	public float health = 100;
	public float maxHealth = 100;
	public float hitDelay = 0.3f;
	public float movementSpeed = 10;
	public float maxMovementSpeed = 25;
	public float deathCounter = 0.5f;
	public int scoreBounty = 10;
	public GameObject healthDrop;
	public int dropRate = 10;
	public SpriteRenderer healthBar; //Health bar sprite to be used

	public Material defMat;
	public Material hitMat;

	protected Vector3 healthVector; //Vector of health bar
	protected float healthScale; //Scale health bar to size of health container.
	protected float hitTime;

	protected bool deathState = false;
	protected Rigidbody2D r;
	// Use this for initialization
	void Start () {
		EntityStart ();
	}

	protected void playerMaterial() {
		/*
		 * call to update after getting hit so that we go back to default material,
		 */ 
		if ((hitTime + 0.1f < Time.time)) {
			GetComponent<SpriteRenderer>().material = defMat;
		}
		
		if (hitTime + 0.1f >= Time.time)
		{
			//Debug.Log("HIT");
			GetComponent<SpriteRenderer>().material = hitMat;
		}
	}

	public void damageEntity(float damage) {
		if (hitTime + hitDelay < Time.time) {
			hitTime = Time.time;
			health -= damage;
		}
	}

	protected void EntityStart() {
		r = GetComponent<Rigidbody2D> ();
		if (healthBar != null) {
			healthVector = healthBar.transform.localScale;
			healthScale = health / maxHealth;
		}
	}
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
	}

	protected void EntityUpdate() {
		HealthUpdate ();
		deathCheck ();
		if (hitMat != null && defMat != null) {
			playerMaterial ();
		}
	}

	protected void HealthUpdate() {
		if (health > maxHealth) {
			health = maxHealth;
		}
		if (health <= 0) {
			health = 0;
		}
		if (healthBar != null) {
			healthScale = health / maxHealth;
			healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
		}
	}

	public virtual void deathCheck() {
		if (!deathState) { //check to see if they are already in death state
			if (health <= 0) {
				deathState = true;
				Utilities.score += scoreBounty;
				if (gameObject.CompareTag("Enemy")) {
					Utilities.enemyCount -= 1;
					if (Random.Range(0, 100) < dropRate && healthDrop != null) {
						Instantiate(healthDrop, transform.position, Quaternion.identity);
					}
				}
				Destroy(gameObject, deathCounter);
				//gameObject.collider2D.enabled = false;
			}
		}
	}
}
