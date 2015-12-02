using UnityEngine;
using System.Collections;

public class ReactivateLimbs : MonoBehaviour {
	public float Timer = 5f; 
	private float curTime;

	void Start() {
		curTime = Time.time;
	}

	void Update() {

		if (curTime + Timer < Time.time) {
			curTime = Time.time;
			foreach(Transform child in gameObject.transform) {
				child.gameObject.SetActive(true);
			}
		}
	}
}
