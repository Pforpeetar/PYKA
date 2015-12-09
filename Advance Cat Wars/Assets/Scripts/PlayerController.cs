using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {
	public float rayCastScale = 1000f;
	public LayerMask layerMask;
	public Material selectedMaterial;
	public Material defaultMaterial;

	private bool movementPhase = false;
	private bool attackPhase = false;
	private bool movingCamera = false;

	private bool unitSelected = false;
	private bool tileSelected = false;

	private UnitPiece selectedUnit;
	private Tile selectedTile;

	private List<GameObject> path;
	private RaycastHit2D originRaycast;
	private RaycastHit2D northRaycast;
	private RaycastHit2D southRaycast;
	private RaycastHit2D westRaycast;
	private RaycastHit2D eastRaycast;

	private Rigidbody2D r;
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

	void inputCheck() {
		mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		
		Debug.DrawRay (new Vector3(mousePos.x, mousePos.y, -5), new Vector3 (0, 0, -1)); 
		if (Input.GetMouseButton (0) || Input.GetMouseButton(1)) {
			originRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			northRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y + 1, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			southRaycast = Physics2D.Raycast (new Vector3 (mousePos.x, mousePos.y - 1, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			westRaycast = Physics2D.Raycast (new Vector3 (mousePos.x - 1, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			eastRaycast = Physics2D.Raycast (new Vector3 (mousePos.x + 1, mousePos.y, 0), new Vector3 (0, 0, -1), rayCastScale, layerMask);
			//Debug.Log (mousePos);
			if (originRaycast.collider) {
				/*
				Debug.Log("Origin:" + originRaycast.collider);
				Debug.Log("North:" + northRaycast.collider);
				Debug.Log("South:" + southRaycast.collider);
				Debug.Log("West:" + westRaycast.collider);
				Debug.Log("East:" + eastRaycast.collider);
				*/
			}
		}

		if (Input.GetMouseButton (0) && originRaycast.collider != null) {
			selectCheck ();
		}

		if (Input.GetMouseButton (1) && originRaycast.collider != null) {
			if (originRaycast.collider.CompareTag("Tile")) {
				selectedTile = originRaycast.collider.gameObject.GetComponent<Tile>();
				if (selectedTile != null) {
					selectedTile.GetComponent<SpriteRenderer> ().material = selectedMaterial;
				}
			} else {
				selectedTile.GetComponent<SpriteRenderer>().material = defaultMaterial;
			}
		}
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

	void selectCheck() {
		if (originRaycast.collider.CompareTag ("UnitPiece")) {
			selectedUnit = originRaycast.collider.gameObject.GetComponent<UnitPiece> ();
			selectedUnit.selected = true;
			selectedUnit.GetComponent<SpriteRenderer> ().material = selectedMaterial;
			unitSelected = true;
		} else if (selectedUnit != null) {
			selectedUnit.selected = false;
			selectedUnit.GetComponent<SpriteRenderer>().material = defaultMaterial;
			unitSelected = false;
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
			if (GUI.Button (new Rect(Screen.width/2 - 100, Screen.height/2, Screen.width/4, Screen.height/20), "Execute")) {
				//print ("Clicked End Game");
				movementPhase = false;
			}
		}
	}


}
