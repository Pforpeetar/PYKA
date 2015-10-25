using UnityEngine;
using System.Collections;

public class Dash : MonoBehaviour {
	private Vector2 boostSpeed = new Vector2(0,0);
	private bool canBoost = true;
	public float boostCooldown = 2f;
	private Direction direction = 0;
	//public GameObject DashTrail;

	void Start() {
		gameObject.GetComponent<TrailRenderer>().enabled=false; 
	}

	void Update()
	{
		if ((canBoost == true) && Input.GetButtonDown ("Slide") && (GetComponent<Rigidbody2D>().velocity.x != 0 || GetComponent<Rigidbody2D>().velocity.y != 0) && PlayerInfo.GetState().Equals(PState.normal))
		{
			StartCoroutine( Boost(.5f) ); //Start the Coroutine called "Boost", and feed it the time we want it to boost us
		}
		
	}

	IEnumerator Boost(float boostDur) //Coroutine with a single input of a float called boostDur, which we can feed a number when calling
	{
		gameObject.GetComponent<TrailRenderer>().enabled=true; 
		float time = 0; //create float to store the time this coroutine is operating
		canBoost = false; //set canBoost to false so that we can't keep boosting while boosting
		//Instantiate(DashTrail, transform.position, Quaternion.identity );
		while(boostDur > time) //we call this loop every frame while our custom boostDuration is a higher value than the "time" variable in this coroutine
		{
			time += Time.deltaTime; //Increase our "time" variable by the amount of time that it has been since the last update
			SetDirection ();
			calcBoostSpeed();
			GetComponent<Rigidbody2D>().velocity = boostSpeed; //set our rigidbody velocity to a custom velocity every frame, so that we get a steady boost direction like in Megaman
			yield return 0; //go to next frame
		}
		yield return new WaitForSeconds(boostCooldown/5*2);
		gameObject.GetComponent<TrailRenderer>().enabled=false; 
		yield return new WaitForSeconds(boostCooldown/5*3); //Cooldown time for being able to boost again, if you'd like.
		canBoost = true; //set back to true so that we can boost again.
	}

	void SetDirection ()
	{
		if (GetComponent<Rigidbody2D>().velocity.x < 0) {
			direction = Direction.left;
		} else if (GetComponent<Rigidbody2D>().velocity.x > 0) {
			direction = Direction.right;
		} else if (GetComponent<Rigidbody2D>().velocity.y < 0) {
			direction = Direction.down;
		} else if (GetComponent<Rigidbody2D>().velocity.y > 0) {
			direction = Direction.up;
		}
	}
	
	void calcBoostSpeed() {
		if (Direction.down == direction) {
			boostSpeed = new Vector2(0,-10);
		} else if (Direction.up == direction) {
			boostSpeed = new Vector2(0,10);
		} else if (Direction.left == direction) {
			boostSpeed = new Vector2(-10,0);
		} else if (Direction.right == direction) {
			boostSpeed = new Vector2(10,0);
		}
	}
}