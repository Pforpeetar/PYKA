using UnityEngine;
using System.Collections;



public class Building : Tile {
	public BuildingType type = BuildingType.Building;
	public Owner ownership = Owner.Null;
	public int captureTime = 20;
	public bool selected = false;
}


