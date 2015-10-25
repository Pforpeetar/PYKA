using UnityEngine;
using System.Collections;

public class MultiTagScript : MonoBehaviour
{

	public string[] objectTags;
	public static string PLAYER = "Player";

	public bool objectHasTag (string tagToCheckFor)
	{
			if ((tagToCheckFor == null) || (objectTags.Length == 0)) {
					return false;
			}
			foreach (string curTag in objectTags) {
					if (curTag.Equals (tagToCheckFor)) {
							return true;
					}
			}
			return false;
	}
}
