using UnityEngine;
using System.Collections;

public class WeaponHandler : MonoBehaviour {
	public ProjectileMovement pM;

	public void createProjectile() {
		pM.movement ();
		pM.hitTime = Time.time;
	}
}
