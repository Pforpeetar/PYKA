using UnityEngine;
using System.Collections;

public class UnitPiece : MonoBehaviour {
	public bool selected = false;
	public int unitSize = 10;
	public int visionRange = 5;
	public int movementRange = 3; //number of tiles can move on. Can't move diagonally. 
	public int minAttackRange = 1;
	public int maxAttackRange = 1; 
	public Owner ownership = Owner.Player1;
}
