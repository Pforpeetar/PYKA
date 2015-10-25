using UnityEngine;
using System.Collections;

public class Entity : OmonoPehaviour {
	public float health = 100;
	public float maxHealth = 100;
	public float damageResistance;
	public float hKnockBackRes;
	public float vKnockBackRes;
	public Material Default;
	public Material Hit;
	public bool displayHealthBar = true;
	protected float hitTime;
	protected bool deathState = false;
	protected SpriteRenderer healthBar; //Health bar sprite to be used
	protected SpriteRenderer healthContainer; //Health container sprite to disable
	protected Vector3 healthVector; //Vector of health bar
	protected float healthScale; //Scale health bar to size of health container.

	// Use this for initialization
	protected override void OnStart () {
		getHealthBar ();
		if (!displayHealthBar) {
			healthBar.renderer.enabled = false;
			healthContainer.renderer.enabled = false;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		EntityUpdate ();
	}

	protected void EntityUpdate() {
		HealthUpdate ();
		playerMaterial ();
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
		if (displayHealthBar) {
			healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
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

	void getHealthBar() {
		//Retrieves health bar sprites in the children of current object
		foreach (Transform child in transform) {
			GameObject hpContainer = child.gameObject;
			healthContainer = hpContainer.GetComponent<SpriteRenderer>();
			foreach (Transform grandChild in child) {
				GameObject hpbar = grandChild.gameObject;
				if (hpbar.CompareTag ("HealthBar")) {
					healthBar = hpbar.GetComponent<SpriteRenderer> ();
					healthVector = healthBar.transform.localScale;
					healthScale = health / maxHealth;
					break;
				}
			}
		}
	}

	protected void playerMaterial() {
		/*
		 * call to update after getting hit so that we go back to default material,
		 */ 
		if ((hitTime + 0.1f < Time.time)) {
			GetComponent<SpriteRenderer>().material = Default;
		}
		
		if (hitTime + 0.1f >= Time.time)
		{
			GetComponent<SpriteRenderer>().material = Hit;
		}
	}
	
	/*protected void callDamage(DamageStruct eStruct) {
		//Debug.Log ("Calling damage");
		damageProperties(eStruct.coll.gameObject, eStruct.damage, eStruct.knockback, eStruct.hitDelay); 
	}*/

	public float GetHealth()
	{
		return health;
	}

	public float GetMaxHealth()
	{
		return maxHealth;
	}


	//CALLED ON OBJECT TAKING DAMAGE!
	public void damageProperties(GameObject collInfo, int damage, float hKnockBack, float vKnockBack, float hitdelay) {
		//GetComponent<SpriteRenderer>().material = Hit;
		if ((hitTime < Time.time)/*&& PlayerInfo.g*/) {
			hitTime = Time.time + hitdelay;

			if (damage - damageResistance >= 0) {
				health = health - (damage - damageResistance);//sets health after damage is applied
			}
			float verticalPush;
			float horizontalPush; //horizontal vector between object and object collided with
			if ((collInfo.transform.position.y - transform.position.y -1f) < 0) {
				verticalPush = -1; //vertical vector between object and object collided with
			} else {
				verticalPush = 1;
			}
			if (collInfo.transform.position.x - transform.position.x < 0) {
				horizontalPush = -1;
			} else {
				horizontalPush = 1;
			}

			float totalHKB = hKnockBack - hKnockBackRes;
			float totalVKB = vKnockBack - vKnockBackRes;

			rigidbody2D.AddForce(new Vector2(-horizontalPush * totalHKB, -verticalPush * totalVKB)); //sets health after damage is applied

			if (!displayHealthBar) //for those that don't display their own health, are relying on HUD observer patterns
			{
				Utilities.SendToListeners(new Message(gameObject, OmonoPehaviour.ms_HEALTHCHANGE));
			}
		}
	}
}
