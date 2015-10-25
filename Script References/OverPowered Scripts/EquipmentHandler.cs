using UnityEngine;
using System.Collections;

public enum ProjType { straight, curve, bounce, columbus, spread, missile, boomerang, chain };
public class EquipmentHandler : OmonoPehaviour {
	public PlayerWeapon Straight;
	public PlayerWeapon Curve;
	public PlayerWeapon Missle;
	public PlayerWeapon Bounce;
	public PlayerWeapon Columbus;
	public PlayerWeapon Spread;
	public EntityStateManager eManager;
	/*public PlayerWeapon Bounce;
	public PlayerWeapon Curve;
	public PlayerWeapon Missle;
	public PlayerWeapon Bounce;*/
	GameObject weaponObject;
	int curryAmmo;
	PlayerWeapon equippedWeapon;

	protected override void OnStart () {
		ChangeEquip(ProjType.straight);
	}

	public int GetCurrAmmo()
	{
		return curryAmmo;
	}

	public PlayerWeapon GetCurrWeapon()
	{
		return equippedWeapon;
	}

	//called by oncollision method of weapon pickup after it grabs the equipment handler component of collided player if it collides with a player
	public void ChangeEquip(ProjType p) 		//sets currentEquip to passed in equipment type
	{
		//TODO: implement weapon change
		PlayerWeapon locWeapon=null; //just for assignment reasons
		if (p.Equals(ProjType.straight))
		{
			locWeapon = Straight;
		}
		else if (p.Equals(ProjType.bounce))
		{
			locWeapon = Bounce;
		}
		else if (p.Equals(ProjType.curve))
		{
			locWeapon = Curve;
		}
		else if (p.Equals(ProjType.missile))
		{
			locWeapon = Missle;
		}
		else if (p.Equals(ProjType.chain))
		{
			locWeapon = Straight;
		}
		else if (p.Equals(ProjType.columbus))
		{
			locWeapon = Columbus;
		}
		else if (p.Equals(ProjType.spread))
		{
			locWeapon = Spread;
		}
		else if (p.Equals(ProjType.boomerang))
		{
			locWeapon = Straight;
		}
		equippedWeapon = locWeapon;
		weaponObject = locWeapon.WeaponPrefab;
		curryAmmo = locWeapon.ammo;
		Utilities.SendToListeners(new Message(gameObject, OmonoPehaviour.ms_EQUIPCHANGE));
	}
	
	public void FireCurrentProjectile()
	{
		//called by pAction when range button is pressed
		GameObject instanceOfRangePrefab = null;
		if (weaponObject != null) { //checks if entity has a range projectile
			instanceOfRangePrefab = (GameObject)GameObject.Instantiate (weaponObject, new Vector3 (10000, 10000), Quaternion.identity);
		}
		instanceOfRangePrefab.SendMessage ("setState", eManager); //too lazy to make struct, send player direction to prefab
		instanceOfRangePrefab.SendMessage ("execute", gameObject); //sends gameobject of entity so projectile knows where to spawn from 
		curryAmmo -= 1;
		Utilities.SendToListeners(new Message(gameObject, OmonoPehaviour.ms_AMMOCHANGE));
		if (curryAmmo == 0)
		{
			ChangeEquip(ProjType.straight);
		}
	}
}
