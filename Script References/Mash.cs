//Carl Lee
//This player object will control most of what is going on in this game.
//Cuz I'm lazy...

using UnityEngine;
using System.Collections;

public class Mash : MonoBehaviour {

	public float mashDuration;		//How long can the player mash?
	public float fadeDuration;		//How long will it take to fade to black?
	public int pressesToWin;		//How much presses needed to pass the level?
	public float winForce;			//Force onto enemy when player wins (WHEEEEEEEEE!!!!)
	public float winForceSpin;		//Torque onto enemy when player wins
	public LevelManager lvlmanager;
	public GameObject mashSlash;
	public GameObject fader;
	public GameObject enemies;
	//Audio
	public AudioClip slashSFX;
	private float lowPitchRange = 0.75f;
	private float highPitchRange = 1.5f;
	private float lowVolRange = 0.8f;
	private bool fading;
	//I should be using Time.deltaTime instead...check shmup notes
	public int timesPressed = 0;	//Counter for # of times pressed, public for camera
	public float currentTime;		//public for camera
	public float endTime;			//public for camera
	private float startLightFade;
	private float endLightFade;
	public GUIStyle lol;
	//Setup Timer
	//Setup Fader
	void Start () {
		currentTime = Time.time;
		endTime = currentTime + mashDuration + 0.5f + fadeDuration; //Difference from endLightFade and endTime is 3.0 seconds
		startLightFade = currentTime + 0.5f;
		endLightFade = currentTime + 0.5f + fadeDuration;
		fading = false;
		Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 0.0f);
		fader.renderer.material.color = newA;
	}
	
	// Update is called once per frame
	void Update () {
	
		//Update currentTime
		currentTime = Time.time;

		//Dim the lights (Fade to black)
		//Interpolation through time formula = A(1-t) + B(t)
		//Alpha 0.0 = Transparent, Alpha 1.0 = Opaque, so A = 0.0 and B = 1.0
		//We have t range from 0 to fadeDuration, but it needs to be scaled to a 0 to 1 range
		//1/fadeDuration * (currentTime - startLightFade)
		if (currentTime > startLightFade && currentTime < endLightFade) {
			fading = true;
			float alpha = (1/fadeDuration) * (currentTime - startLightFade); //Never a negative number thankts to currentTime > startLightFade
			Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, alpha);
			fader.renderer.material.color = newA;
		}

		//Lights out, Allowed to mash when currentTime is within 3 seconds of the endTime
		//Also stop enemy movement
		bool mashCheck = false;
		if (endTime - currentTime < mashDuration && endTime - currentTime > 0.0f) {
			mashCheck = true;
			//Last transparency check on fader, make sure it is opaque
			if (fader.renderer.material.color.a != 1.0f) {
				Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 1.0f);
				fader.renderer.material.color = newA;
			}
			//For each enemy, stop their momentum
			//ATM, empty gameObject called enemies holds enemy1, enemy2, etc...
			foreach (Transform enemy in enemies.gameObject.transform) {
				enemy.gameObject.rigidbody2D.velocity = Vector2.zero;
				enemy.gameObject.rigidbody2D.gravityScale = 0; //Disable gravity on them
			}
		}

		//If pushing "Action" and mashCheck == true...0
		//Spawn a slash effect for each press
		//Play a sound for each press (Or I could link sound with the prefab spawning...)
		if (Input.GetButtonDown ("Action") && mashCheck) {
			timesPressed++;
			//Spawn prefab
			Vector3 newPos = new Vector3 (Random.Range (-2.0f, 2.0f), Random.Range (-3.0f, 0.0f), -2.0f);
			Quaternion newRot = Quaternion.Euler (0, 0, Random.Range (0.0f, 360.0f));
			Instantiate(mashSlash, newPos, newRot);
			//Counter Debug
			Debug.Log (timesPressed);
			//SFX
			audio.pitch = Random.Range (lowPitchRange,highPitchRange);
			float hitVol = Random.Range (lowVolRange, 1.0f);
			audio.PlayOneShot(slashSFX,hitVol);
		}

		//Akuma mode ended, lights back on
		if (currentTime > endTime) {
			//When lights go back on, give the enemies momentum as well, depends on win or lose
			if (fader.renderer.material.color.a != 0.0f) {
				Color newA = new Color (fader.renderer.material.color.r, fader.renderer.material.color.g, fader.renderer.material.color.b, 0.0f);
				fader.renderer.material.color = newA;


				//Start moving enemies again (Control it here, I'm dumb and I don't know how to call other object's functions)
				foreach (Transform enemy in enemies.gameObject.transform) {
					enemy.gameObject.rigidbody2D.gravityScale = 1; //Re-enable gravity (I dont think this works anymore >_>)

					//Enemies will fly toward the player on lose, away from player on win.
					if (timesPressed >= pressesToWin) { //Win
						//Get vector facing away from the player current position
						//enemyPosition - playerPosition
						Vector2 newDir = enemy.gameObject.transform.position - transform.position;
						newDir.Normalize ();
						enemy.gameObject.rigidbody2D.AddForce(newDir * winForce);
						//Add torque, enemy on left side gets positive torque
						if (enemy.gameObject.transform.position.x < 0) {
							enemy.gameObject.rigidbody2D.AddTorque(winForceSpin);
						}
						else {
							enemy.gameObject.rigidbody2D.AddTorque(-winForceSpin);
						}

					}
					else { //Lose
						//Get vector facing towards the player current position
						//playerPosition - enemyPosition
						Vector2 newDir = transform.position - enemy.gameObject.transform.position;
						newDir.Normalize ();
						enemy.gameObject.rigidbody2D.AddForce(newDir * winForce);
					}
				}
			}
		}

		//1 second delay after above, check win/lose
		if (currentTime > endTime + 1.0f) {
			//Check win
			if (timesPressed >= pressesToWin) {
				lvlmanager.CompleteLevel();
			}
			//Else lose
			else {
				lvlmanager.FailLevel();
			}
		}}

	void OnGUI()
	{
		if (fading) {
			GUI.Label(new Rect(Screen.width -70,Screen.height-70,50,50), ""+timesPressed,lol);
		}
	}
		/*void OnGUI()
		{
			if (fading) {GUI.Label(new Rect(Screen.width -60,Screen.height-60,50,50), timesPressed,Utilities.LevelDisplay(null)));
		}}*/
	}


