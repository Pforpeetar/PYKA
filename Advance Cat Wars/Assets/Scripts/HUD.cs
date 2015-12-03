using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI() {
		//click start turn call gamemanager.startturn
		if (GameManager.turn.Equals (Owner.Null)) {
			//display next player's name and button to start turn;

		}
	}
}
