using UnityEngine;
using System.Collections;

public class HealthPickup : Pickupable {
	public int increaseHealth;
	// Use this for initialization

	//when they player touches this object, destroy it and increase player's health
	public override void OnTriggerEnter2D(Collider2D whatICollidedWith)
	{
		if (Utilities.hasMatchingTag("Player",whatICollidedWith.gameObject))
		{
			//Debug.Log ("Player health increased by: " + increaseHealth + " to: " + PlayerInfo.getHealth());
			PlayerInfo.changeHealth(increaseHealth);
			Utilities.TellPlayer("Health Restored!");
			Utilities.playSound(mySound); //sound type inherited from interface
			Destroy(this.gameObject);
		}
	}
}
