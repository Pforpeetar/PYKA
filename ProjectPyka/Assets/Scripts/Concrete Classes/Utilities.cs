using UnityEngine;
using System.Collections;

public static class Utilities {
	public static int score;

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
