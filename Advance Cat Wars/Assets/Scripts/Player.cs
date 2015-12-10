using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	//playerEnum used to determine what the current player turn is.
	public Owner playerEnum; 

	public int money = 100;
	//unitPieces used to keep track of available pieces left on the board and to regenerate them if they're on a building.
	//Maybe have two lists of pieces, one for player1 and another for player2
	public List<UnitPiece> unitPieces = new List<UnitPiece>(); 

	// Update is called once per frame
	void Update () {
		if (GameManager.turn.Equals (playerEnum)) {
			//do this
		}
	}

	//Perform specific functions at the start of each player's turn.
	public void initTurn() {
		updateUnits ();
	}

	//Regenerate any unit's health when they are on a building.
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
