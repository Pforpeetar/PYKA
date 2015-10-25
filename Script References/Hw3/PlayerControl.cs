using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour {
	public int speed = 10;
	public int health = 100;
	public float rayCastScale = .75f;
	public LayerMask layerMask;
	private Rigidbody2D rigidBody;
	public Material Default;
	public Material Hit;
	public float hitDelay = 1f;
	private bool isGrounded = true;
	private float hitTime;
	private bool dead = false;
	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (health <= 0) {
			Time.timeScale = 0;
			dead = true;
		}
		rigidBody.velocity = new Vector2 (Input.GetAxis("Horizontal")*speed, rigidBody.velocity.y);

		RaycastHit2D downRayCast = Physics2D.Raycast(gameObject.transform.position, new Vector2(0,-1),rayCastScale, layerMask);
		RaycastHit2D rightRayCast = Physics2D.Raycast(gameObject.transform.position, new Vector2(1,0),rayCastScale, layerMask);
		RaycastHit2D leftRayCast = Physics2D.Raycast(gameObject.transform.position, new Vector2(-1,0),rayCastScale, layerMask);
		if (Input.GetButtonDown ("Jump") && isGrounded) {
			rigidBody.velocity = new Vector2 (rigidBody.velocity.x, 10);
			isGrounded = false;
		}
		if (downRayCast.collider || rightRayCast.collider || leftRayCast.collider) {
			isGrounded = true;
		} else {
			isGrounded = false;
		}

		if (hitTime + 0.1f < Time.time) {
			GetComponent<SpriteRenderer>().material = Default;
		}
	}

	void OnCollisionStay2D(Collision2D collInfo) {
		if (collInfo.collider.tag == "Enemy") {
			Enemy p = collInfo.gameObject.GetComponent<Enemy>();
			Rigidbody2D r2 = collInfo.gameObject.GetComponent<Rigidbody2D>();
			GetComponent<SpriteRenderer>().material = Hit;
			if ((hitTime + hitDelay < Time.time)/*&& PlayerInfo.g*/) {
				hitTime = Time.time;
			}
		}
	}

	void OnGUI() {
		GUI.Label (new Rect (50, 25, 100, 100), "Health: " + health);
		if (dead) {
			GUI.Label (new Rect (100, 100, 100, 100), "You died");
		}
	}
}
