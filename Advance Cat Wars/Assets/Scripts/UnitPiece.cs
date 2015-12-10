using UnityEngine;
using System.Collections;

public class UnitPiece : MonoBehaviour {
	public bool finishedMovement = false;
	public bool finishedAttack = false;
	//sorry
	public bool selected = false;
	public int maxUnitSize = 10;
	public int unitSize = 0;
	public int regenRate = 2;
	public int visionRange = 5;
	public int movementRange = 3; //number of tiles can move on. Can't move diagonally. 
	public int minAttackRange = 1;
	public int maxAttackRange = 1; 
	public Owner ownership = Owner.Player1;

	void Start() {
		unitSize = maxUnitSize;
	}

	void Update() {
		if (unitSize <= 0) {
			Destroy(gameObject);
		}
	}

	public bool isOnBuilding() {
		// raycast down...
		//   if building, return true
		//   else return false
		return false;
	}

	public void regenUnitSize() {
		// regen if less than max
		if (unitSize < maxUnitSize) {
			unitSize += regenRate;
		}

		// if unitSize larger than max, set unitSize to max
		if (unitSize > maxUnitSize) {
			unitSize = maxUnitSize;
		}

		// update num display - NOT IMPLEMENTED 
		// maxUnit size does not display
	}
}
