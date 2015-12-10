using UnityEngine;
using System.Collections;

public class Factory : Building {

	public GameObject catToSpawn;

	public bool hasCat() {
		originRaycast = Physics2D.Raycast (new Vector3 (transform.position.x, transform.position.y, transform.position.z), new Vector3 (0, 0, 1), 1000f, layerMask);
		if (originRaycast.collider != null && originRaycast.collider.gameObject.CompareTag("UnitPiece")) {
			return true;
		}
		return false;
	}

	public void spawnCat() {
		Vector3 pos = gameObject.transform.position;
		GameObject spawnedCat = (GameObject) Instantiate(catToSpawn, new Vector3(pos.x, pos.y, -6f), Quaternion.identity);
	}
}
