using UnityEngine;
using System.Collections;
using System.IO;
using System;

public static class Utilities
{
		public static bool inDebugMode;
		public static GameObject GUIBroadCaster;
		public static GameObject EffectPlayer;
		public static void saveGame ()
		{
				//Debug.Log (Application.dataPath);
				try {
						String savePath = Application.dataPath + "/test.blanksav";
						StreamWriter saver = new StreamWriter (savePath, false);
						saver.WriteLine ("Save data stores player's last scene, list of unlocked spells, and last position");
						saver.WriteLine (Application.loadedLevelName);
						String delimiter = "";
						foreach (Spell s in SpellBook.playerSpells) {
								saver.Write (delimiter + s.isSpellUnlocked ());
								delimiter = "|";
						} 		
						saver.Write ("\n");
						GameObject playaPlaya = GameObject.FindGameObjectWithTag ("Player");
						Vector3 playaPos = playaPlaya.transform.position;
						saver.WriteLine (playaPos.x + delimiter + playaPos.y + delimiter + playaPos.z);
						Debug.Log ("Game Saved (sorta) to " + savePath + ".");
						saver.Close ();
				} catch (Exception e) {
						//TODO: handle exception
						Debug.Log ("Error writing save file: " + e.Message);
				}
		}

		public static void loadGame ()
		{
				try {
						String savePath = Application.dataPath + "/test.blanksav";
						StreamReader loader = new StreamReader (savePath, false);
						loader.ReadLine ();
						String currLevelName = loader.ReadLine ();
						intializeSpellBook (inDebugMode);
						String[] unlockBools = loader.ReadLine ().Split ('|');
						int length = unlockBools.Length;
						int i;
						bool testBool;
						for (i = 0; i < length; i++) {
								Boolean.TryParse (unlockBools [i], out testBool);				
								if (testBool) {
										SpellBook.playerSpells [i].unlockSpell ();
								}
						}
						String[] coordinates = loader.ReadLine ().Split ('|');
						float x, y, z;
						float.TryParse (coordinates [0], out x);
						float.TryParse (coordinates [1], out y);
						float.TryParse (coordinates [2], out z);
						//Vector3 playaPos = new Vector3(x,y,z);
						loader.Close(); 
						Application.LoadLevel(currLevelName);
						//TODO: move plaver to position of loading
				} catch (Exception e) {
						//TODO: handle exception
						Debug.Log ("Error reading save file: " + e.Message);
				}
		}

/**
* Used to instantiate any object at the passed in position
* Pass in a velocity vector if the object should move
* Pass in a orientation vector so that the sprite can be rotated accordingly
*/ 
		public static GameObject cloneObject (Direction direction, GameObject bulletToClone, Vector3 placetoCreate, Vector3 velocity, Quaternion orientation)
		{		
				GameObject clonedesu = (GameObject)ScriptableObject.Instantiate (bulletToClone, placetoCreate, orientation);
				if (clonedesu.GetComponent<Rigidbody2D>()) {
						clonedesu.GetComponent<Rigidbody2D>().velocity = velocity;
				}
				if(clonedesu.GetComponent<AudioSource>())
				{
					clonedesu.GetComponent<AudioSource>().Play();
				}
				return clonedesu;
		}

		public static void intializeSpellBook (bool debugMode)
		{
				inDebugMode = debugMode;
				if (SpellBook.size () > 0) {
						return;
				} //just in case I'm trying to add to a spell book that's already there.
				RedSpell spellObj = (RedSpell)ScriptableObject.CreateInstance ("RedSpell");
				spellObj.initializeSpell (SpellBook.REDSPELLNAME, "Has a tendency to burn things", 30, 10,2,90,2);
				spellObj.unlockSpell ();
				SpellBook.add (spellObj);
				YellowSpell spellObj2 = (YellowSpell)ScriptableObject.CreateInstance ("YellowSpell");
				spellObj2.initializeSpell (SpellBook.YELLOWSPELLNAME, "Charges things", 30, 10, 2,120,3);
				SpellBook.add (spellObj2);
				BlueSpell spellObj3 = (BlueSpell)ScriptableObject.CreateInstance ("BlueSpell");
				spellObj3.initializeSpell (SpellBook.BLUESPELLNAME, "Useful on water", 100, 1,200,3);
				SpellBook.add (spellObj3);
				WhiteSpell spellObj4 = (WhiteSpell)ScriptableObject.CreateInstance ("WhiteSpell");
				spellObj4.initializeSpell (SpellBook.WHITESPELLNAME, "Aggressive guide", 300, 30,300,8);
				SpellBook.add (spellObj4);
				if (debugMode) { //can switch flag at the top of game manager so everything acts in a debug mode to make testing easier
						spellObj2.unlockSpell ();
						spellObj3.unlockSpell ();
						spellObj4.unlockSpell ();
						PlayerInfo.SetCanCharge(true);
				}

		}

	/*
	 * Calls flipStatus method on all switchable objects that are children to parent transform
	 */
	public static void flipStatusInChildren(Transform parentTransform)
	{
		foreach (Transform child in parentTransform) {
			GameObject goldenMegatron = child.gameObject;
			if (hasMatchingTag("Switchable",goldenMegatron)) {
				goldenMegatron.SendMessage ("flipStatus");
			}
		}
	}

	/*
	 * Calls setStatus methof on all switchable objects that are children to parent transform
	 */
	public static void setStatusInChildren(Transform parentTransform, bool status)
	{
		foreach (Transform child in parentTransform) {
			GameObject goldenMegatron = child.gameObject;
			if (hasMatchingTag("Switchable", goldenMegatron)) {
				goldenMegatron.SendMessage ("setStatus", status);
			}
		}
	}

	/*
	 * Checks passed in Game object to see if any of it's tags are of the requested value
	 */ 
	public static bool hasMatchingTag(string tagToCheckFor, GameObject objectToCheck)
	{
		MultiTagScript mult =  objectToCheck.GetComponent<MultiTagScript>();
		if (mult != null) { return mult.objectHasTag(tagToCheckFor);}
		return false;
	}

	/*
	 * Method for displaying a non-modal message to the player on the screen
	 * message will appear in the top right and fade away after a set period of time
	 */ 
	public static void TellPlayer(string whatToTellThem)
	{
		if (GUIBroadCaster == null) { 
			GUIBroadCaster = GameObject.FindGameObjectWithTag("GUIBroadCaster");
		}
		GUIBroadCaster.BroadcastMessage("BroadcastNewMessage",whatToTellThem);
	}
	public static void playSound(SoundType typeofSoundToPlay)
	{
		if (EffectPlayer == null) { 
			EffectPlayer = GameObject.FindGameObjectWithTag("SoundEffectPlayer");
		}
		EffectPlayer.BroadcastMessage("playSoundEffect",typeofSoundToPlay);
	}

	public static void rotateObject(Direction direction, GameObject obj) {
		if (direction == Direction.up) {
			obj.transform.Rotate(new Vector3(0, 0, 90));
		}
		if (direction == Direction.down) {
			obj.transform.Rotate(new Vector3(0, 0, 270));
		}
		if (direction == Direction.right) {
			obj.transform.Rotate(new Vector3(0, 0, 0));
		}
		if (direction == Direction.left) {
			obj.transform.Rotate(new Vector3(0, 0, 180));
		}
	}
}

