using UnityEngine;
using System.Collections;

public class UnitPiece : MonoBehaviour {
	public bool finished = false;
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
	private RaycastHit2D originRaycast;
	public LayerMask layerMask;

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
		originRaycast = Physics2D.Raycast (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (0, 0, 1), 1000f, layerMask);
		if (originRaycast.collider != null && originRaycast.collider.gameObject.CompareTag("Building")) {
			if (originRaycast.collider.gameObject.GetComponent<Building>().ownership != ownership) {
				originRaycast.collider.gameObject.GetComponent<Building>().captureTime -= 10;
			}
			return true;
		}


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
