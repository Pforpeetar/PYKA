using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public int health = 100;
	public GameObject target;
	public int speed = 5;
	private Rigidbody2D r;
	private float hitTime;
	public float hitDelay = 1f;
	public float knockBack = 1000;
	public Material Default;
	public Material Hit;
	// Use this for initialization
	void Start () {
		r = this.gameObject.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (health <= 0) {
			Destroy (gameObject);
		}

		r.AddForce ((target.transform.position - transform.position) * speed);

		if (r.velocity.x > 5) {
			r.velocity = new Vector3(5, r.velocity.y, 0);
		}
		if (r.velocity.y > 5) {
			r.velocity = new Vector3(r.velocity.x, 5, 0);
		}
		if (r.velocity.x < -5) {
			r.velocity = new Vector3(-5, r.velocity.y, 0);
		}
		if (r.velocity.y < -5) {
			r.velocity = new Vector3(r.velocity.y, -5, 0);
		}
		if (hitTime + 0.2f < Time.time) {
			GetComponent<SpriteRenderer>().material = Default;
		}
	}

	void OnCollisionStay2D(Collision2D collInfo) {
		if (collInfo.collider.tag == "Player") {
			PlayerControl p = collInfo.gameObject.GetComponent<PlayerControl>();
			Rigidbody2D r2 = collInfo.gameObject.GetComponent<Rigidbody2D>();
			if ((hitTime + hitDelay < Time.time)/*&& PlayerInfo.g*/) {
				hitTime = Time.time;
				p.health -= 10;
				float verticalPush = collInfo.gameObject.transform.position.y - transform.position.y;
				float horizontalPush = collInfo.gameObject.transform.position.x - transform.position.x;
				r.AddForce(new Vector2(-horizontalPush, -verticalPush) * knockBack);
			}
		}
		if (collInfo.collider.tag == "PlayerProjectile") {
			GetComponent<SpriteRenderer>().material = Hit;
			if ((hitTime < Time.time)/*&& PlayerInfo.g*/) {
				hitTime = Time.time;
				float verticalPush = collInfo.gameObject.transform.position.y - transform.position.y;
				float horizontalPush = collInfo.gameObject.transform.position.x - transform.position.x;
				r.AddForce(new Vector2(-horizontalPush, -verticalPush) * knockBack);
			}
		}
	}
}
