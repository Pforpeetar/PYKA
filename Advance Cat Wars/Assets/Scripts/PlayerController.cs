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

	private UnitState curUnitState = UnitState.Start;

	//Phases act like states. Certain options will be available depending on what state.
	private bool startPhase = true;
	private bool movementPhase = false;
	private bool attackPhase = false;
	private bool movingCamera = false;

	//Check to see if a unit or tile is currently selected.
	private bool unitSelected = false;
	private bool tileSelected = false;

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
		if (movementPhase) {
			selectTile ();
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
			if (originRaycast.collider.gameObject.GetComponent<UnitPiece>().ownership.Equals(GameManager.opposingPlayer) && attackPhase) {
				selectedEnemy = originRaycast.collider.gameObject.GetComponent<UnitPiece> ();
				selectedEnemy.GetComponent<SpriteRenderer> ().material = selectedMaterial;
			}
		}
	}

	void selectUnit() {
		//Selecting logic
		if (Input.GetMouseButtonDown (0)) {
			//Debug.Log(originRaycast.collider);
			if (selectedUnit != null && selectedUnit.ownership.Equals(GameManager.currentPlayer)) {
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

	void switchTurn() {
		if (GameManager.currentPlayer.Equals(Owner.Player1)) {
			GameManager.currentPlayer = Owner.Player2;
			GameManager.opposingPlayer = Owner.Player1;
		} 
		else {
			GameManager.currentPlayer = Owner.Player1;
			GameManager.opposingPlayer = Owner.Player2;
		}
		unitSelected = false;
		tileSelected = false;
		startPhase = true;
		attackPhase = false;
		selectedUnit.finishedAttack = false;
		selectedUnit.finishedMovement = false;
		selectedUnit.GetComponent<SpriteRenderer> ().material = defaultMaterial;
		movementPhase = false;
		selectedUnit = null;
		selectedTile = null;
		curUnitState = UnitState.Start;
	}

	//text for the score
	void OnGUI() {
		Vector3 pos = Camera.main.WorldToScreenPoint(Input.mousePosition);

		//make 4 buttons
		//make methods to toggle visiblity
		if (unitSelected) {
			if (curUnitState == UnitState.Start) {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, Screen.width / 4, Screen.height / 20), "Start Turn")) {
					curUnitState = UnitState.Move;
				}
			}
			if (curUnitState == UnitState.Move) {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 25, Screen.width / 4, Screen.height / 20), "Start Move")) {
					curUnitState = UnitState.Attack;
				}
			}
			if (curUnitState == UnitState.Attack) {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 50, Screen.width / 4, Screen.height / 20), "Start Attack")) {
					curUnitState = UnitState.End;
				}
			}

			if (curUnitState == UnitState.End) {
				if (GUI.Button (new Rect (Screen.width / 2, Screen.height / 2 + 100, Screen.width / 4, Screen.height / 20), "End Unit Turn")) {
					switchTurn();
				}
			}
		}

		if (selectedUnit != null) {
			pos = Camera.main.WorldToScreenPoint (selectedUnit.transform.position);
		}

		GUI.Label (new Rect (Screen.width/2, Screen.height/12, 100, 100), "Turn: " + GameManager.currentPlayer.ToString());

		/*
		if (GUI.Button (new Rect(Screen.width / 1.25f, Screen.height / 1.25f, Screen.width/4, Screen.height/20),"EndTurn")) {

		}



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
