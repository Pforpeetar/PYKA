using UnityEngine;
using System.Collections;

public static class Utilities {

	public static Transform getPlayerTransform() {
		if (GameObject.FindGameObjectWithTag ("Player") != null) {
			return GameObject.FindGameObjectWithTag ("Player").gameObject.transform;
		}
		return null;
	}
}

public enum EntityType{
	Player,Enemy
}
