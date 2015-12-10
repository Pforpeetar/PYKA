using UnityEngine;
using System.Collections;



public class Building : Tile {
	protected RaycastHit2D originRaycast;
	public LayerMask layerMask;
	public BuildingType type = BuildingType.Building;
	public Owner ownership = Owner.Null;
	public int captureTime = 20;
	public Material player1Material;
	public Material player2Material;
	public Material defaultMaterial;

	void Update() {
		if (ownership == Owner.Player1) {
			gameObject.GetComponent<SpriteRenderer>().material = player1Material;
		}
		else if (ownership == Owner.Player2) {
			gameObject.GetComponent<SpriteRenderer> ().material = player2Material;
		} else {
				gameObject.GetComponent<SpriteRenderer>().material = defaultMaterial;
		}
		originRaycast = Physics2D.Raycast (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (0, 0, -1), 1000f, layerMask);
		//Debug.DrawRay (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (0, 0, -1));
		//Debug.Log (originRaycast.collider);
		if (captureTime <= 0) {

			if (originRaycast.collider != null && originRaycast.collider.CompareTag("UnitPiece")) {
				if (originRaycast.collider.gameObject.GetComponent<UnitPiece>().ownership != ownership) {
					ownership = originRaycast.collider.gameObject.GetComponent<UnitPiece>().ownership;
				}
			}
			captureTime = 20;
		}
	}
}


