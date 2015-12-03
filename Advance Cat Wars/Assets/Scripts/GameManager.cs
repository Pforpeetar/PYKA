using UnityEngine;
using System.Collections;

public enum Owner {
	Null, Player1, Player2};

public enum BuildingType {
	HeadQuarter, Factory, Building};

public static class GameManager {
	public static Player[] playerArray;
	public static Owner turn = Owner.Null;
	
	public static void startGame() {
		turn = Owner.Player1;
	}

	public static void intermediateTurn() {
		//This is a black screen before next players turn. 
		turn = Owner.Null;
	}

	public static void startTurn() {

	}
}
