using UnityEngine;
using System.Collections;

public class MeleeManager : OmonoPehaviour {
	public StrikeCollision hitBox; //get the StrikeBox to determine attack hit detection
	public string TargetType; //specify target type
	
	public DamageStruct groundMelee1;
	public DamageStruct groundMelee2;
	public DamageStruct groundMelee3;
	public DamageStruct airMelee;
	public DamageStruct launcher;
	public DamageStruct diveBomb;
	public DamageStruct spinToWin;

	private EntityStateManager eManager;

	void Start() {
		eManager = gameObject.GetComponent<PlayerAction> ().eManager; //get the statemanager to determine attack state.
	}

	public void getAttackData() {
		//Debug.Log("Is colliding");
		AState currAstate = eManager.GetAttackState ();
		if (currAstate.Equals (AState.attack1)) //check current attack state
			hitBox.setDamageInfo (groundMelee1, TargetType); //send the targettype and respective damage struct
		else if (currAstate.Equals (AState.attack2))
			hitBox.setDamageInfo (groundMelee2, TargetType);
		else if (currAstate.Equals (AState.attack3))
			hitBox.setDamageInfo (groundMelee3, TargetType);
		else if (currAstate.Equals (AState.airattack))
			hitBox.setDamageInfo (airMelee, TargetType);
		else if (currAstate.Equals (AState.debug))
			hitBox.setDamageInfo (diveBomb, TargetType);
		else if (currAstate.Equals (AState.manup))
			hitBox.setDamageInfo (launcher, TargetType);
		else if (currAstate.Equals (AState.spintowin))
			hitBox.setDamageInfo (spinToWin, TargetType);
		else if (currAstate.Equals (AState.normal)) 
			hitBox.setDamageInfo(new DamageStruct(0,0,0,0,"Hanabi"),"Hanabi"); //this isbecause of the launcher jump cancel double hit bug nonsense
	}

}