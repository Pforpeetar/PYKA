using UnityEngine;
using System.Collections;

public enum WeaponState {
	Pistol, Spread, Missle
}

public class Player : Entity {
	public WeaponHandler wH;
	public float mana = 100;
	public float maxMana = 100;
	public GameObject lightning;
	private Animator animator;
	private int weaponIndex = 0;
	public WeaponState state = WeaponState.Pistol;
	// Use this for initialization
	void Start () {
		EntityStart ();
		animator = GetComponent<Animator> ();
		health = Utilities.currentHealth;
	}
	
	// Update is called once per frame
	void Update () {
		EntityUpdate ();
		inputCheck ();
		animationCheck ();
		rotateCharacterToCursor ();

		if (r.velocity.x > maxMovementSpeed) {
			r.velocity = new Vector2(maxMovementSpeed, r.velocity.y);
		} if (r.velocity.y > maxMovementSpeed) {
			r.velocity =  new Vector2(r.velocity.x, maxMovementSpeed);
		}
		mana += 5 * Time.deltaTime;
		if (mana >= maxMana) {
			mana = maxMana;
		}
	}

	void animationCheck() {

		if (state.Equals(WeaponState.Pistol)){
			animator.SetBool ("SpreadWalk", false);
			animator.SetBool ("SpreadIdle", false);
			animator.SetBool ("MissleWalk", false);
			animator.SetBool ("MissleIdle", false);
			if (r.velocity.x != 0 || r.velocity.y != 0) {
				animator.SetBool ("PistolWalk", true);
				animator.SetBool ("PistolIdle", false);
			} else {
				animator.SetBool ("PistolWalk", false);
				animator.SetBool ("PistolIdle", true);
			}
		}

		if (state.Equals(WeaponState.Spread)) {
			animator.SetBool ("PistolWalk", false);
			animator.SetBool ("PistolIdle", false);
			animator.SetBool ("MissleWalk", false);
			animator.SetBool ("MissleIdle", false);
			if (r.velocity.x != 0 || r.velocity.y != 0) {
				animator.SetBool ("SpreadWalk", true);
				animator.SetBool ("SpreadIdle", false);
			} else {
				animator.SetBool ("SpreadWalk", false);
				animator.SetBool ("SpreadIdle", true);
			}
		}

		if (state.Equals(WeaponState.Missle)) {
			animator.SetBool ("PistolWalk", false);
			animator.SetBool ("PistolIdle", false);
			animator.SetBool ("SpreadWalk", false);
			animator.SetBool ("SpreadIdle", false);
			if (r.velocity.x != 0 || r.velocity.y != 0) {
				animator.SetBool ("MissleWalk", true);
				animator.SetBool ("MissleIdle", false);
			} else {
				animator.SetBool ("MissleWalk", false);
				animator.SetBool ("MissleIdle", true);
			}
		}
	}

	void inputCheck() {
		r.velocity = new Vector2 (Input.GetAxis("Horizontal")*movementSpeed, Input.GetAxis("Vertical")*movementSpeed);

		if (Input.GetMouseButton (0) && (wH.pM[weaponIndex].hitTime + wH.pM[weaponIndex].shotCooldown < Time.time)) {
			wH.createProjectile(weaponIndex);
			//pM.hitTime = Time.time;
		}
		
		if (Input.GetMouseButtonDown (1)) {
			weaponIndex++;
			if (weaponIndex == 1) 
				state = WeaponState.Spread;
			if (weaponIndex == 2) 
				state = WeaponState.Missle;
			if (weaponIndex >= wH.pM.Count) {
				state = WeaponState.Pistol;
				weaponIndex = 0;
			}
		}

		if (Input.GetKey (KeyCode.F) && mana >= 10) {

			mana -= 30 * Time.deltaTime;
			if (mana >= 10) {
				lightning.SetActive (true);
			}
			if (mana <= 0) {
				mana = 0;
			}
		} else {
			lightning.SetActive (false);
		}


	}

	void rotateCharacterToCursor() {
		Vector3 mousePos = Input.mousePosition;
		
		Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle+90));
	}

}
