using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float rayCastScale = 1000f;
	public LayerMask layerMask;
	//Material tile/unit set to when selected
	public Material selectedMaterial;
	//Default sprite material
	public Material defaultMaterial;

	//Phases act like states. Certain options will be available depending on what state.
	private bool movementPhase = false;
	private bool attackPhase = false;
	private bool movingCamera = false;

	//Check to see if a unit or tile is currently selected.
	private bool unitSelected = false;
	private bool tileSelected = false;

	//Current selected objects
	private UnitPiece selectedUnit;
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
	}

	//Method to hold all player mouse inputs and their functionality.
	void inputCheck() {
		rayCastCheck ();
		selectUnit ();
		if (movementPhase && currentMovement < selectedUnit.movementRange) {
			selectTile ();
		}
		cameraMovement ();
	}

	void selectUnit() {
		//Selecting logic
		if (Input.GetMouseButton (0) && !unitSelected) {
			Debug.Log(originRaycast.collider);
			if (originRaycast.collider != null && originRaycast.collider.CompareTag ("UnitPiece")) {
				selectedUnit = originRaycast.collider.gameObject.GetComponent<UnitPiece> ();
				selectedUnit.selected = true;
				selectedUnit.GetComponent<SpriteRenderer> ().material = selectedMaterial;
				unitSelected = true;
			} 
			else if (selectedUnit != null){
				selectedUnit.selected = false;
				selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
				unitSelected = false;
			}
		}
	}

	void selectTile() {
		if (Input.GetMouseButton (1)) {
			if (originRaycast.collider.CompareTag("Tile") && originRaycast.collider != null) {
				currentMovement++;
				selectedTile = originRaycast.collider.gameObject.GetComponent<Tile>();
				if (selectedTile != null) {
					selectedTile.GetComponent<SpriteRenderer> ().material = selectedMaterial;
				}
			} else {
				selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
				tileSelected = false;
				currentMovement--;
			}
		}
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

	void buildPath() {
		
	}

	void executePath() {
		
	}

	//text for the score
	void OnGUI() {
		if (unitSelected) {
			if (GUI.Button (new Rect(Camera.main.WorldToScreenPoint(selectedUnit.transform.position).x, Camera.main.WorldToScreenPoint(selectedUnit.transform.position).y, Screen.width/4, Screen.height/20),"Start Move")) {
				movementPhase = true;
				unitSelected = false;
			}

		}
		if (movementPhase) {
			Debug.Log("poop");
			if (GUI.Button (new Rect(Camera.main.WorldToScreenPoint(selectedUnit.transform.position).x, Camera.main.WorldToScreenPoint(selectedUnit.transform.position).y, Screen.width/4, Screen.height/20), "Execute")) {
				//print ("Clicked End Game");
				movementPhase = false;
			}
		}
	}


}
