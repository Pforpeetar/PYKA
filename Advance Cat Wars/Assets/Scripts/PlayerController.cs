using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum UnitState {
	Start, Move, Attack, End
}

public class PlayerController : MonoBehaviour {
	public float rayCastScale = 1000f;
	public LayerMask layerMask;
	//Material tile/unit set to when selected
	public Material selectedMaterial;
	//Default sprite material
	public Material defaultMaterial;
	//Finished material to signal that a unit is finished.
	public Material finishMaterial;

	private UnitState curUnitState = UnitState.Start;

	//Phases act like states. Certain options will be available depending on what state.
	private bool startPhase = true;
	private bool movementPhase = false;
	private bool attackPhase = false;
	private bool movingCamera = false;

	//Check to see if a unit or tile is currently selected.
	private bool unitSelected = false;
	private bool tileSelected = false;
	private bool enemySelected = false;
	//Current selected objects
	private UnitPiece selectedUnit;
	private UnitPiece selectedEnemy;
	private Tile selectedTile;

	//List of gameobjects to build a path for the unit piece to follow
	private List<GameObject> path;

	//Raycast to collide with object on mouse cursor.
	private RaycastHit2D originRaycast;
	private RaycastHit2D northRaycast;
	private RaycastHit2D southRaycast;
	private RaycastHit2D westRaycast;
	private RaycastHit2D eastRaycast;

	private Rigidbody2D r;
	private int currentMovement = 0;
	public float cameraSpeed = 1f;

	Vector3 mousePos = new Vector3(0, 0, 0);
	
	// Use this for initialization
	void Start () {
		r = gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		inputCheck ();
		Debug.Log ("CurrentPlayer: " + GameManager.currentPlayer +  " OpposingPlayer: " + GameManager.opposingPlayer);
	}

	//Method to hold all player mouse inputs and their functionality.
	void inputCheck() {
		rayCastCheck ();
		if (startPhase) {
			selectUnit ();
		}
		if (curUnitState == UnitState.Move) {
			selectTile();
		}
		if (curUnitState == UnitState.Attack) {
			selectEnemyUnit();
		}
		//cameraMovement ();
	}

	//Fires a raycast from the mouse and returns data from any object it collides with.
	void rayCastCheck() {
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		Debug.DrawRay (new Vector3(mousePos.x, mousePos.y, -5), new Vector3 (0, 0, -1)); 
		if (Input.GetMouseButton (0) || Input.GetMouseButton(1)) {
			originRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			northRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y + 1, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			southRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y - 1, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			westRaycast = Physics2D.Raycast (new Vector3 (mousePos.x - 1, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			eastRaycast = Physics2D.Raycast (new Vector3 (mousePos.x + 1, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
		}
		if (originRaycast.collider != null && originRaycast.collider.CompareTag ("Tile")) {
			selectedTile = originRaycast.collider.gameObject.GetComponent<Tile> ();
		}
		if (originRaycast.collider != null && originRaycast.collider.CompareTag ("UnitPiece")) {
			if (originRaycast.collider.gameObject.GetComponent<UnitPiece>().ownership.Equals(GameManager.currentPlayer)) {
				selectedUnit = originRaycast.collider.gameObject.GetComponent<UnitPiece> ();
			}
		}
	}

	void selectEnemyUnit() {
		if (Input.GetMouseButtonDown (0)) {
			if (originRaycast.collider != null && originRaycast.collider.CompareTag ("UnitPiece")) {
				if (originRaycast.collider.gameObject.GetComponent<UnitPiece>().ownership.Equals(GameManager.opposingPlayer)) {
					selectedEnemy = originRaycast.collider.gameObject.GetComponent<UnitPiece> ();
					if (checkAdjacentRaycast(selectedUnit.transform.position, selectedEnemy.transform.position)) {
						selectedEnemy.GetComponent<SpriteRenderer> ().material = selectedMaterial;
						enemySelected = true;
					} else {
						selectedEnemy = null;
					}
				}
			}
		}
	}

	void selectUnit() {
		//Selecting logic
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log(originRaycast.collider);
			if (selectedUnit != null && selectedUnit.ownership.Equals(GameManager.currentPlayer) && !selectedUnit.finished) {
				if(!unitSelected) {
					if (originRaycast.collider != null && originRaycast.collider.CompareTag ("UnitPiece")) {
						selectedUnit.selected = true;
						selectedUnit.GetComponent<SpriteRenderer> ().material = selectedMaterial;
						unitSelected = true;
						Debug.Log("Unit Selected. ");
					} 
				}
			}
		}
	}

	void selectTile() {
		if (Input.GetMouseButtonDown (1)) {
			if (!tileSelected 
			    && selectedTile.MoveableOnto 
			    && checkAdjacentRaycast (selectedUnit.transform.position, selectedTile.transform.position)) {
				if (originRaycast.collider.CompareTag ("Tile") && originRaycast.collider != null) {
					selectedTile.selected = true;
					selectedTile.GetComponent<SpriteRenderer> ().material = selectedMaterial;
					tileSelected = true;
				} 
			}
		}
	}

	/*
	 * @param unitPos is your selected origin position
	 * @param adjPos are the positions north, south, west and east of the origin pos.
	 */
	bool checkAdjacentRaycast(Vector3 unitPos, Vector3 adjPos) {
		//Vector3 unitPos = selectedUnit.transform.position;
		//Vector3 tilePos = selectedTile.transform.position;
		if (
		((unitPos.x + 1 == adjPos.x) && (unitPos.y == adjPos.y))
		|| ((unitPos.y + 1 == adjPos.y) && (unitPos.x == adjPos.x))
		|| ((unitPos.x - 1 == adjPos.x) && (unitPos.y == adjPos.y))
		|| ((unitPos.y - 1 == adjPos.y) && (unitPos.x == adjPos.x))
			)
		{
			return true;
		} return false;
	}
	
	void cameraMovement() {
		//Moving camera logic
		Vector3 pos = transform.position;
		
		if (Input.GetMouseButtonDown (1) && !unitSelected && !tileSelected) {
			movingCamera = true;
		}
		
		if (movingCamera) {
			
			r.velocity = new Vector3 ((mousePos.x - pos.x) * cameraSpeed, (mousePos.y - pos.y) * cameraSpeed, 0);
		} else {
			r.velocity = new Vector3(0, 0, 0);
		}
		
		if (Input.GetMouseButtonUp (1)) {
			movingCamera = false;
		}
	}

	void reset() {
		selectedUnit.finished = true;
		selectedUnit.GetComponent<SpriteRenderer> ().material = finishMaterial;
		unitSelected = false;
		tileSelected = false;
		startPhase = true;
		attackPhase = false;
		curUnitState = UnitState.Start;
	}

	void switchTurn() {
		if (GameManager.currentPlayer.Equals(Owner.Player1)) {
			GameManager.currentPlayer = Owner.Player2;
			GameManager.opposingPlayer = Owner.Player1;
		} 
		else {
			GameManager.currentPlayer = Owner.Player1;
			GameManager.opposingPlayer = Owner.Player2;
		}
		reset ();
		foreach (GameObject u in GameObject.FindGameObjectsWithTag("UnitPiece")) {
			u.GetComponent<UnitPiece>().finished = false;
			u.GetComponent<SpriteRenderer>().material = defaultMaterial;
		}
		selectedUnit.finished = false;	
		curUnitState = UnitState.Start;
	}

	//May break if isn't checked properly
	void toggleTile() {
		selectedTile.GetComponent<SpriteRenderer> ().material = defaultMaterial;
	}

	void toggleEnemy() {
		selectedEnemy.GetComponent<SpriteRenderer> ().material = defaultMaterial;
	}

	//text for the score
	void OnGUI() {
		Vector3 pos = Camera.main.WorldToScreenPoint(Input.mousePosition);

		//make 4 buttons
		//make methods to toggle visiblity
		if (unitSelected) {
			pos = Camera.main.WorldToScreenPoint (selectedUnit.transform.position);
			GUI.Label (new Rect (pos.x, pos.y - 25, 100, 100), "Health: " + selectedUnit.unitSize);

			if (curUnitState == UnitState.Start) {
				if (GUI.Button (new Rect (pos.x, pos.y, Screen.width / 4, Screen.height / 20), "Start Move")) {
					curUnitState = UnitState.Move;
				}
				if (GUI.Button (new Rect (pos.x, pos.y + 25, Screen.width / 4, Screen.height / 20), "Cancel")) {
					unitSelected = false;
					selectedUnit.GetComponent<SpriteRenderer> ().material = defaultMaterial;
					selectedUnit = null;
				}
			}
			if (curUnitState == UnitState.Move) {
				if (GUI.Button (new Rect (pos.x, pos.y, Screen.width / 4, Screen.height / 20), "Next: Attack")) {
					curUnitState = UnitState.Attack;
				}

				if (tileSelected) {
					if (GUI.Button (new Rect (pos.x, pos.y + 25, Screen.width / 4, Screen.height / 20), "Okay")) {
						selectedUnit.transform.position = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y, selectedUnit.transform.position.z);
						curUnitState = UnitState.Attack;
						toggleTile();
					}
				}

			}
			if (curUnitState == UnitState.Attack) {
				if (GUI.Button (new Rect (pos.x, pos.y, Screen.width / 4, Screen.height / 20), "Next: End")) {
					curUnitState = UnitState.End;
				}
				if (enemySelected) {
					if (GUI.Button (new Rect (pos.x, pos.y + 25, Screen.width / 4, Screen.height / 20), "Okay")) {
						selectedEnemy.unitSize -= 5;
						curUnitState = UnitState.End;
						toggleEnemy();
					}
				}
			}

			if (curUnitState == UnitState.End) {
				if (GUI.Button (new Rect (pos.x, pos.y, Screen.width / 4, Screen.height / 20), "End Unit Turn")) {
					reset ();
				}
			}
		}

		GUI.Label (new Rect (Screen.width/2, Screen.height/12, 100, 100), "Turn: " + GameManager.currentPlayer.ToString());


		if (GUI.Button (new Rect(0, 0, Screen.width/4, Screen.height/20), "End Turn")) {
			switchTurn();
		}


		/*
		if (unitSelected) {
			GUI.Label (new Rect (pos.x, pos.y - 25, 100, 100), "Health: " + selectedUnit.unitSize);
			if (GUI.Button (new Rect(pos.x, pos.y + 50, Screen.width/4, Screen.height/20), "Cancel")) {
				//print ("Clicked End Game");
				//selectedUnit.finishedAttack = true;
				movementPhase = false;
				attackPhase = false;
				unitSelected = false;
				tileSelected = false;
				startPhase = true;
				selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
				//selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
			}
			if (!selectedUnit.finishedMovement) {
				if (GUI.Button (new Rect(pos.x, pos.y, Screen.width/4, Screen.height/20),"Start Move")) {
					movementPhase = true;
					unitSelected = false;
					startPhase = false;
					attackPhase = true;
					selectedUnit.finishedMovement = true;
				}
			}
			if (!selectedUnit.finishedAttack) {
				if (GUI.Button (new Rect(pos.x, pos.y + 25, Screen.width/4, Screen.height/20),"Start Attack")) {
					movementPhase = false;
					unitSelected = false;
					startPhase = false;
					attackPhase = true;
					selectedUnit.finishedAttack = true;
				}
			}

		}

		if (attackPhase) {
			if (selectedEnemy != null && checkAdjacentRaycast (selectedUnit.transform.position, selectedEnemy.transform.position)) {
				if (GUI.Button (new Rect(pos.x, pos.y, Screen.width/4, Screen.height/20), "Execute")) {
					//print ("Clicked End Game");
					selectedUnit.finishedAttack = true;
					movementPhase = false;
					unitSelected = false;
					tileSelected = false;
					attackPhase = false;
					startPhase = true;
					selectedEnemy.GetComponent<SpriteRenderer>().material = defaultMaterial;
					selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
					if (selectedEnemy != null) {
						Debug.Log("KILLL MEEEE PLS");
						selectedEnemy.unitSize -= selectedUnit.unitSize / 2;
					}
				}
			}
			if (GUI.Button (new Rect(pos.x, pos.y + 50, Screen.width/4, Screen.height/20), "Cancel")) {
				//print ("Clicked End Game");
				//selectedUnit.finishedAttack = true;
				movementPhase = false;
				attackPhase = true;
				unitSelected = false;
				tileSelected = false;
				startPhase = true;
				selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
				selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
			}
		}

		if (movementPhase) {
			if (tileSelected) {
				if (GUI.Button (new Rect(pos.x, pos.y, Screen.width/4, Screen.height/20), "Execute")) {
					//print ("Clicked End Game");
					selectedUnit.finishedMovement = true;
					movementPhase = false;
					unitSelected = false;
					tileSelected = false;
					startPhase = true;
					selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
					selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
					selectedUnit.transform.position = new Vector3(selectedTile.transform.position.x, selectedTile.transform.position.y, selectedUnit.transform.position.z);
				}
				if (GUI.Button (new Rect(pos.x, pos.y + 50, Screen.width/4, Screen.height/20), "Cancel")) {
					//print ("Clicked End Game");
					//selectedUnit.finishedMovement = true;
					movementPhase = true;
					attackPhase = false;
					unitSelected = false;
					tileSelected = false;
					startPhase = true;
					selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
					selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
				}
			}
			if (GUI.Button (new Rect(pos.x, pos.y + 50, Screen.width/4, Screen.height/20), "Cancel")) {
				//print ("Clicked End Game");
				//selectedUnit.finishedMovement = true;
				movementPhase = true;
				attackPhase = false;
				unitSelected = false;
				tileSelected = false;
				startPhase = true;
				selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
				selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
			}
		}
		*/
	}

	/* not used
	void cancel(Vector3 pos) {
		if (GUI.Button (new Rect(pos.x, pos.y + 50, Screen.width/4, Screen.height/20), "Cancel")) {
			//print ("Clicked End Game");
			movementPhase = false;
			attackPhase = false;
			unitSelected = false;
			tileSelected = false;
			startPhase = true;
			selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
			selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
		}
	}
	*/

	void toggle(bool var) {
		if (var) {
			var = false;
		} else {
			var = true;
		}
	}

}
