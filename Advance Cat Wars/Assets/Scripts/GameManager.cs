using UnityEngine;
using System.Collections;

public enum Owner {
	Null, Player1, Player2};

public enum BuildingType {
	HeadQuarter, Factory, Building};

public static class GameManager {
	public static Player[] playerArray;
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
}
