using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
		public static GameManager gM;
		public GameObject healthPickup;
		public GameObject manaPickup;
		public GameObject powerPickup;
		public GameObject speedPickup;
		public GameObject invulnPickup;
		public bool debugMode = true;

		void Start() {
		}

		// Use this for initialization
		void Awake ()
		{
				if (!gM) {
						gM = this;
						DontDestroyOnLoad (gameObject);
				} else {
						Destroy (gameObject);
				}
				Utilities.intializeSpellBook (debugMode);
		}
		
	void OnLevelWasLoaded(int level) {
		/**
		 * If current level matches checkpoint level, player has died and and the checkpoint data needs reloading
		 * If current level does not match checkpoint level, checkpoint data needs saving because player got here naturally
		 */
		if (level == CheckPointState.CPlevel)
		{
			PlayerInfo.setHealth(CheckPointState.CPhealth);
			PlayerInfo.setMana(CheckPointState.CPmana);
			SpellBook.setSpellBools(CheckPointState.CPspellBools);
			PlayerInfo.SetCanCharge(CheckPointState.CPchargeEnabled);
		}
		else
		{
			CheckPointState.UpdateState(PlayerInfo.getHealth(),PlayerInfo.getMana(),level,SpellBook.getSpellBools(),PlayerInfo.CanCharge());
		}
	}
		/*pass in the x,y,and z coordinates of the place where you want the pickup to be dropped
	pickup needs to exist in unity and be a child prefab of the game manager prefab
	Ex: when maybe a monster dies or something, pass in the position of the transform of the monster*/
		void DropHealthPickup (Vector3 locationToDrop)
		{
				Instantiate (healthPickup, locationToDrop, new Quaternion ());
		}

		/* same as health above*/
		void DropManaPickup (Vector3 locationToDrop)
		{
				Instantiate (manaPickup, locationToDrop, new Quaternion ());
		}

	void DropPowerPickup (Vector3 locationToDrop)
	{
		Instantiate (powerPickup, locationToDrop, new Quaternion ());
	}
	
	void DropSpeedPickup (Vector3 locationToDrop)
	{
		Instantiate (speedPickup, locationToDrop, new Quaternion ());
	}

	void DropInvulnPickup (Vector3 locationToDrop)
	{
		Instantiate (invulnPickup, locationToDrop, new Quaternion ());
	}
	
	void InvokePowerReset()
	{
		Invoke("PowerDown",10);
	}

	void InvokeSpeedReset()
	{
		Invoke("SpeedDown",10);
	}

	void InvokeInvulnReset()
	{
		Invoke("InvulnDown",10);
	}

	void PowerDown()
	{
		PlayerInfo.PowerDown();
	}

	void SpeedDown()
	{
		PlayerInfo.SpeedDown();
	}

	void InvulnDown()
	{
		PlayerInfo.InvulnDown();
	}
		// Update is called once per frame
		void Update ()
		{

	
		}
}
