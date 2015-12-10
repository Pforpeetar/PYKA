using UnityEngine;
using System.Collections;

public enum Owner {
	Null, Player1, Player2};

public enum BuildingType {
	HeadQuarter, Factory, Building};

public static class GameManager {
	public static Player[] playerArray;
	public static int turnCount = 0;
	public static Owner currentPlayer = Owner.Player1;
	public static Owner opposingPlayer = Owner.Player2;
	public static void startGame() {
		currentPlayer = Owner.Player1;
	}

	public static void intermediateTurn() {
		//This is a black screen before next players turn. 
		currentPlayer = Owner.Null;
	}

	public static void startTurn() {

	}

	public static void updateUnits() {
		UnitPiece tempUnit;
		foreach(GameObject unit in GameObject.FindGameObjectsWithTag("UnitPiece")) {
			tempUnit = unit.GetComponent<UnitPiece>();
			//Debug.Log(tempUnit);
			if (tempUnit != null && tempUnit.isOnBuilding()) {
				Debug.Log ("Regen units");
				tempUnit.regenUnitSize();
			}
		}
	}

	public static void updateFactories() {
		Factory tempFactory;
		foreach (GameObject factory in GameObject.FindGameObjectsWithTag("Building")) {
			if (factory.GetComponent<Factory>() != null) {
				tempFactory = factory.GetComponent<Factory> ();
				//Debug.Log (factory);
				if (!tempFactory.hasCat()) {
					tempFactory.spawnCat ();
				}
			}
		}

	}
}
