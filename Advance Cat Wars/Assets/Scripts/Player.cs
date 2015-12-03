using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	public Owner playerEnum; 
	public List<UnitPiece> unitPieces = new List<UnitPiece>(); 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.turn.Equals (playerEnum)) {
			//do this
		}
	}
	
	public void initTurn() {
		updateUnits ();
	}

	public void updateUnits() {
		UnitPiece tempUnit;
		foreach(UnitPiece unit in unitPieces) {
			tempUnit = unit;
			if(tempUnit.isOnBuilding()) {
				tempUnit.regenUnitSize();
			}
		}
	}
}
