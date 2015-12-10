using UnityEngine;
using System.Collections;

public class HomingModule : MonoBehaviour {
	public EntityType target = EntityType.Enemy;
	public float homingRadius = 5;
	public float homingSpeed = 20;
	private Transform targetTransform = null;
	public GameObject parent;

	void Update() {
		//Debug.Log(GameObject.FindGameObjectWithTag(target.ToString()));
		if ((parent != null) && (GameObject.FindGameObjectWithTag(target.ToString()) != null)) {
			if (findTarget() && targetTransform != null) {
				Chasing (parent.GetComponent<Rigidbody2D> ());
				Vector3 targetPos = Camera.main.WorldToScreenPoint (targetTransform.position);
			
				Vector3 objectPos = Camera.main.WorldToScreenPoint (parent.transform.position);
				targetPos.x = targetPos.x - objectPos.x;
				targetPos.y = targetPos.y - objectPos.y;
			
				float angle = Mathf.Atan2 (targetPos.y, targetPos.x) * Mathf.Rad2Deg;
				parent.transform.rotation = Quaternion.Euler (new Vector3 (0, 0, angle + 270));
			}
		}
	}

	bool findTarget() {
		targetTransform = GameObject.FindGameObjectWithTag (target.ToString()).gameObject.transform;
		Vector3 distance = targetTransform.position - parent.transform.position;
		if (Mathf.Abs (distance.x) < homingRadius  && Mathf.Abs(distance.y) < homingRadius) {
			return true;
		}
		return false;
	}

	void Chasing (Rigidbody2D r)
	{
		float Xdif = targetTransform.position.x - parent.transform.position.x;
		float Ydif = targetTransform.position.y - parent.transform.position.y;
		Vector2 Playerdirection = new Vector2 (Xdif, Ydif);
		r.velocity = (Playerdirection.normalized * homingSpeed);
		//r.AddForce (Playerdirection.normalized * 300);
	}


}
