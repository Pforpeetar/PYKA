using UnityEngine;
using System.Collections;

public abstract class BulletDesu : OmonoPehaviour {
	protected Animator animator;
	public float animationDuration;
	public int damageValue; //how much damage they do
	public string TargetType; //Set to Player or Enemy in Inspector
	public float hKnockBack; //for knockback
	public float vKnockBack;
	public float projectileDuration;
	public float hitDelay; //for a delay between when another hit would register 
	public bool destroyOnCollision;
	//public AudioClip bulletSound;
	// Use this for initialization

	protected override void OnStart() {
		animator = (Animator)GetComponent ("Animator");
		if (TargetType == null) {TargetType = "Entity";}
		Destroy (gameObject,projectileDuration);
	}

	public abstract void execute (GameObject g);

	protected void OnCollisionEnter2D(Collision2D collInfo) {
		//Debug.Log(collInfo.gameObject.name);		
		//if (animator) {
		//	animator.SetBool ("Does Collide", true);} //not all objects that this script is attached to have animations
		if (Utilities.hasMatchingTag(TargetType, collInfo.gameObject))
		{
			//DamageStruct thisisntastructanymore = new DamageStruct(damageValue,collider2D.gameObject,knockBackVelocity,hitDelay);
			//struct used to pass more than one parameter through send message, which only lets you pass one object as a parameter
			collInfo.gameObject.SendMessage("callDamage",SendMessageOptions.DontRequireReceiver);
			collInfo.gameObject.GetComponent<Entity>().damageProperties(gameObject, damageValue,hKnockBack, vKnockBack, hitDelay);
		}
		if (destroyOnCollision == true) {
			gameObject.collider2D.enabled = false; //once it hits one object it should no longer be able to hit another object
			rigidbody2D.velocity = new Vector2(0,0);
			Destroy(gameObject, animationDuration);
		}
		Destroy(gameObject, projectileDuration);
	}
}
