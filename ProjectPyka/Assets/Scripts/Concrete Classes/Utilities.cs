using UnityEngine;
using System.Collections;

public static class Utilities {

	public static Transform getPlayerTransform() {
		return GameObject.Find("Player").gameObject.transform;
	}
}

public enum EntityType{
	Player,Enemy
}
