using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WeaponHandler : MonoBehaviour {
	public List<ProjectileMovement> pM;
	public void createProjectile(int currentIndex) {
		pM[currentIndex].movement();
		pM[currentIndex].hitTime = Time.time;
	}
}
