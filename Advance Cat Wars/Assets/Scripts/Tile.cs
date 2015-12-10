using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {
	//Determines if tile is selected or not.
	public bool selected = false;
	//Determines if a tile can be moved onto or not.
	public bool MoveableOnto = true;
	//public float defenseValue = 1;
	//isVisible used to hide a tile if covered by fog of war.
	public bool isVisible = false;
}
