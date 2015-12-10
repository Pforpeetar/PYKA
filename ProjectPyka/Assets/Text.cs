using UnityEngine;
using System.Collections;

public class Text : MonoBehaviour {
	public GUIStyle style;
	private string grade;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width/2 - 100, Screen.height/2 - 75, 100, 100), "SCORE: " + Utilities.score, style);
		if (Utilities.score < 250) {
			grade = "F";
		}
		if (Utilities.score >= 250 && Utilities.score < 500) {
			grade = "D";
		}
		if (Utilities.score >= 500 && Utilities.score < 750) {
			grade = "C";
		}
		if (Utilities.score >= 750 && Utilities.score < 1000) {
			grade = "B";
		}
		if (Utilities.score >= 1000) {
			grade = "A";
		}
		GUI.Label (new Rect (Screen.width/2 - 100, Screen.height/2, 100, 100), "GRADE: " + grade, style);
	}
}
