using UnityEngine;
using System.Collections;

public class StrikeCollision : OmonoPehaviour {
	private int damageValue;
	private float hKnockBack;
	private float vKnockBack;
	private float hitDelay;
	private string TargetType;
	private MeleeManager m;
	public GameObject collisionEffect;

	void Start() {
		if (transform.parent.GetComponentInParent<MeleeManager>() != null)
			m = transform.parent.GetComponent<MeleeManager> (); //get reference to melee manager to signal when collision occurs.
	}

	public void setDamageInfo(DamageStruct d, string t) { //get damage properties of corresponding attack type.
		damageValue = d.damage;
		hKnockBack = d.hKnockback;
		vKnockBack = d.vKnockback;
		hitDelay = d.hitDelay;
		TargetType = t;
	}

	void OnTriggerEnter2D(Collider2D collInfo) {
		//Debug.Log("Is colliding");
		if (m != null)
			m.getAttackData ();
		if (Utilities.hasMatchingTag (TargetType, collInfo.gameObject)) {
			RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.forward, 0.5f, Utilities.EntiOnlyLMask);
			/*if (hit)
			{
				//Debug.Log("Point of contact: " + hit.point);
				GameObject cloneObject = (GameObject) GameObject.Instantiate (collisionEffect, hit.point, Quaternion.identity);
				Destroy (cloneObject, 0.25f);
			}*/
			GameObject cloneObject = (GameObject) GameObject.Instantiate (collisionEffect, collInfo.transform.position, Quaternion.identity);
			Destroy (cloneObject, 0.25f);
			collInfo.gameObject.GetComponent<Entity>().damageProperties(gameObject, damageValue, hKnockBack, vKnockBack, hitDelay);
			collInfo.gameObject.SendMessage("callDamage",SendMessageOptions.DontRequireReceiver);
		}
	}
}
