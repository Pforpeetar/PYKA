using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public Owner playerEnum; 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.turn.Equals (playerEnum)) {
			//do this
		}


	}

	public void startTurn() {
	
	}
}
