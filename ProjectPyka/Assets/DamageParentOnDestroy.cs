using UnityEngine;
using System.Collections;

public class DamageParentOnDestroy : MonoBehaviour {

	void OnDestroy() {
		GetComponentInParent<Entity> ().health -= 1000;
	}
}
