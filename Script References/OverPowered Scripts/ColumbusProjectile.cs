using UnityEngine;
using System.Collections;

public class ColumbusProjectile : StraightProjectile {

	float xLeftBound;
	float xRightBound;
	// Use this for initialization
		protected override void OnStart () {
		base.OnStart();
		GameObject potentialCamera = GameObject.FindGameObjectWithTag("MainCamera");
		Camera camComponent = potentialCamera.GetComponent<Camera>();
		if (camComponent)
		{  
			//this calculates what the actual distance of the camera's range
			float Reach = Camera.main.orthographicSize * Screen.width / Screen.height; 
			xRightBound = Camera.main.transform.position.x + Reach; //gets you the right bound of the camera's visison
			xLeftBound = Camera.main.transform.position.x - Reach; //left bound calculated similarly
			//Debug.Log(xLeftBound);
			//Debug.Log(xRightBound);
		}
		else //just some "playing it safe numbers in case we can't grab the component"
		{
			xLeftBound = -2000f;
			xRightBound = 2000f;
			Debug.Log("For some reason we couldn't grab the main Camera component.");
		}
		//Debug.Log(holla.transform.position.x-holla.pixelWidth/2);
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.x < xLeftBound -.5)
		{
			transform.position = new Vector3(xRightBound,transform.position.y,transform.position.z);
		}
		else if (transform.position.x > xRightBound +.5)
		{
			transform.position = new Vector3(xLeftBound,transform.position.y,transform.position.z);
		}
	}
}
	
	