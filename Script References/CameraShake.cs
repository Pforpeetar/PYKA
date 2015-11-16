using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour {

	public GameObject player;		//Player holds the timer in the Mash game
	public float shakeSTR;			//Strength of screen shake
	public float shakeDuration;		//How long it lasts

	private float startShakeDuration;	//What the shakeDuration was at the start
	private float startingZPos;			//Keep old Z Pos

	// Use this for initialization
	void Start () {
		startShakeDuration = shakeDuration;
		startingZPos = transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {

		//If it is a win, and the lights are on, and shakeDuration is not over, shake camera
		if (player.GetComponent<Mash>().timesPressed >= player.GetComponent<Mash>().pressesToWin &&
		    player.GetComponent<Mash>().currentTime > player.GetComponent<Mash>().endTime &&
		    shakeDuration > 0.0f) {

			//Tick shakeDuration down to zero
			shakeDuration -= Time.deltaTime;

			//shakeDuration/startShakeDuration = Range from 1 to 0
			float intensity = shakeDuration/startShakeDuration;
			intensity = intensity * shakeSTR;
			Vector3 newPos = new Vector3(Random.insideUnitSphere.x * intensity, Random.insideUnitSphere.y * intensity, startingZPos);
			transform.position = newPos;


		}
		else { //Leave camera at normal spot
			transform.position = new Vector3(0.0f, 0.0f, startingZPos);
		}
	}
}
