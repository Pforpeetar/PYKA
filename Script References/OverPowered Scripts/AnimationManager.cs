using UnityEngine;
using System.Collections;

public class AnimationManager : OmonoPehaviour {
	//EState entityState;
	//AState attackState;
	protected Animator animator;
	protected PlayerAction pAction;
	protected EntityStateManager eManager;
	protected GroundChecker pGroundChecker;
	protected float comboDelay;
	protected GameObject rbVel;

	// Use this for initialization
	
	protected override void OnStart () {
		animator = (Animator)GetComponent ("Animator");
		//pAction = (PlayerAction)GetComponent("PlayerAction");
		eManager = (EntityStateManager)GetComponent ("EntityStateManager");
		pGroundChecker = (GroundChecker)GetComponent ("GroundChecker");
		//eManager = pAction.eManager;

		//pGroundChecker = pAction.pGroundChecker;
	}
	// Update is called once per frame
	protected void Update () {
		idleState ();
		jumpState ();
		attackState ();
		dashState ();
	}

	protected void idleState() {
		//Idle State Animations
		if (eManager.Equals (EState.normal) && pGroundChecker.isGrounded() && gameObject.rigidbody2D.velocity.x == 0) {
			animator.SetBool("walkRight", false);
		}
	}


	protected void jumpState() {
		//Jump State Animations
		if (pGroundChecker.isGrounded ()) {
			animator.SetBool("Jump", false);
		} else {
			animator.SetBool("Jump", true);
			animator.SetFloat("vSpeed", gameObject.rigidbody2D.velocity.y);
		}
	}

	protected void dashState() {
		if (eManager.GetEntityState().Equals(EState.dashing)) {
			animator.SetBool ("Dash", true);
		} else {
			animator.SetBool("Dash", false);
		}
	}

	protected void attackState() {
		animator.SetBool("Dive",false);
		animator.SetBool("Launcher",false);
		switch (eManager.GetAttackState ()) {
		case AState.normal:
			playAnimation("default");
			break;
		case AState.attack1:
			playAnimation("Melee1");
			break;
		case AState.attack2:
			//Debug.Log("Attack2");
			playAnimation("Melee2");
			break;
		case AState.attack3:
			//Debug.Log("Attack3");
			playAnimation("Melee3");
			break;
		case AState.shooting:
			playAnimation("Shoot");
			break;
		case AState.debug:
			playAnimation("Dive");
			break;
		case AState.manup:
			playAnimation("Launcher");
			break;
		case AState.spintowin:
			playAnimation("SpinToWin");
			break;
		case AState.airattack:
			playAnimation ("AirMelee");
			break;
		default:
			playAnimation("default");
			//animator.SetBool("walkRight", false);
			break;
		}
	}

	/*
	 * Sets specified attack animation to true, the rest to false. 
	 */
	protected void playAnimation(string anim) {
		if (!anim.Equals ("default")) {
			animator.SetBool (anim, true);
		}
		if (!anim.Equals ("Melee1"))
			animator.SetBool("Melee1", false);
		if (!anim.Equals ("Melee2"))
			animator.SetBool("Melee2", false);
		if (!anim.Equals ("Melee3"))
			animator.SetBool("Melee3", false);
		if (!anim.Equals ("Shoot"))
			animator.SetBool("Shoot", false);
		if (!anim.Equals ("Dive"))
			animator.SetBool("Dive", false);
		if (!anim.Equals ("SpinToWin"))
			animator.SetBool("SpinToWin", false);
		if (!anim.Equals ("Launcher"))
			animator.SetBool("Launcher", false);
		if (!anim.Equals ("AirMelee"))
			animator.SetBool("AirMelee", false);
	}

	protected void setMelee1False() {
		if (eManager.Equals(AState.attack1)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setMelee2False() {
		if (eManager.Equals(AState.attack2)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setMelee3False() {
		if (eManager.Equals(AState.attack3)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setShootFalse() {
		if (eManager.Equals(AState.shooting)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setLauncherFalse() {
		if (eManager.Equals(AState.manup)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setSpinToWinFalse() {
		if (eManager.Equals(AState.spintowin)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setAirMeleeFalse() {
		if (eManager.Equals(AState.airattack)) {
			eManager.SetAttackState (AState.normal);
		}
	}

	protected void setAStateToNormal()
	{
		eManager.SetAttackState (AState.normal);
	}
}
