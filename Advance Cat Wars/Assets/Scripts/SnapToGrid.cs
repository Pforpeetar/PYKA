using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class SnapToGrid : MonoBehaviour {
	// Use this for initialization
	public float gridSize = 0.5f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = transform.position;
		gameObject.transform.position = new Vector3(Mathf.Round(currentPos.x) + gridSize, Mathf.Round(currentPos.y) + gridSize, Mathf.Round(currentPos.z) + gridSize);
	}
}
