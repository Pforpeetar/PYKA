using UnityEngine;
using System.Collections;

public class HUD : MonoBehaviour {
	public SpriteRenderer healthBar; //Health bar sprite to be used
	public SpriteRenderer manaBar;
	public SpriteRenderer[] icons = new SpriteRenderer[3];
	public GUIStyle style;
	private Player player;
	private Vector3 healthVector; //Vector of health bar
	private float healthScale; //Scale health bar to size of health container.
	private Vector3 manaVector; //Vector of health bar
	private float manaScale; //Scale health bar to size of health container.

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		if (healthBar != null) {
			healthVector = healthBar.transform.localScale;
			healthScale = player.health / player.maxHealth;
		}
		if (manaBar != null) {
			manaVector = manaBar.transform.localScale;
			manaScale = player.mana / player.maxMana;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (GameObject.FindGameObjectsWithTag ("Player") != null && player != null) {
			player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		}
		if (healthBar != null) {
			healthScale = player.health / player.maxHealth;
			healthBar.transform.localScale = new Vector3 (healthVector.x * healthScale, 1, 1);
		}
		if (manaBar != null) {
			manaScale = player.mana / player.maxMana;
			manaBar.transform.localScale = new Vector3 (manaVector.x * manaScale, 1, 1);
		}
		if (player.state == WeaponState.Pistol) {
			icons[0].enabled = true;
			icons[1].enabled = false;
			icons[2].enabled = false;
		}
		if (player.state == WeaponState.Spread) {
			icons[0].enabled = false;
			icons[1].enabled = true;
			icons[2].enabled = false;
		}
		if (player.state == WeaponState.Missle) {
			icons[0].enabled = false;
			icons[1].enabled = false;
			icons[2].enabled = true;
		}
	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width/3, Screen.height/10, 100, 100), "SCORE: " + Utilities.score, style);
	}
}
