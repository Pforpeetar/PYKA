using UnityEngine;
using System.Collections;

public class HomingModule : MonoBehaviour {
	private GameObject homingTarget = null;
	public GameObject parent;

	void Update() {
		if (parent != null) {
			if (homingTarget != null) {
				Chasing (parent.GetComponent<Rigidbody2D> ());
				Vector3 targetPos = Camera.main.WorldToScreenPoint (homingTarget.transform.position);
			
				Vector3 objectPos = Camera.main.WorldToScreenPoint (parent.transform.position);
				targetPos.x = targetPos.x - objectPos.x;
				targetPos.y = targetPos.y - objectPos.y;
			
				float angle = Mathf.Atan2 (targetPos.y, targetPos.x) * Mathf.Rad2Deg;
				parent.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 270));
			}
		}
	}

	void Chasing (Rigidbody2D r)
	{
		float Xdif = homingTarget.transform.position.x - parent.transform.position.x;
		float Ydif = homingTarget.transform.position.y - parent.transform.position.y;
		Vector2 Playerdirection;
		Vector3 playerTransform; 
		
		Playerdirection = new Vector2 (Xdif, Ydif);
		r.velocity = (Playerdirection.normalized * 20);
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.tag.Equals ("Enemy")) {
			homingTarget = coll.gameObject;
		}
	}

}
