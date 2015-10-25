using UnityEngine;
using System.Collections;

public class CurveProjectile : BulletDesu {
	public int projectileSpeed=0; //for spell that shoot something out
	protected GameObject entity;
	protected EntityStateManager entityStates;
	private Vector3 clonePosition;
	private Vector3 cloneVelocity;
	private Quaternion cloneOrientation;
	private Direction direction;
	private GameObject bulletToClone;
	public float frequency = 20.0f;  // Speed of sine movement
	public float magnitude = 0.5f;   // Size of sine movement
	private Vector3 axis;
	private Vector3 pos;

	public void OnStart() {
		base.OnStart ();
		pos = transform.position;
		axis = transform.up;  
	}

	public void setState(EntityStateManager e) {
		entityStates = e;
	}

	public override void execute(GameObject g) {
		entity = g;
		direction = entityStates.GetDirState ();
		bulletToClone = gameObject;
		if (entity == null){entity = GameObject.FindGameObjectWithTag("Entity");}
		createProjectile (0.5f, 0, 0, 0);
	}

	void Update () {
		pos += transform.right * Time.deltaTime * projectileSpeed;
		transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;
	}
	
	public void createProjectile(float xPos, float yPos, float rotation, float angle) {
		if (direction == Direction.left) {
			clonePosition = entity.transform.position + new Vector3(-xPos, yPos, 0);
			cloneVelocity = new Vector3 (-projectileSpeed, angle, 0);
			cloneOrientation = Quaternion.Euler(0, 0, rotation + 180);
		}
		else if (direction == Direction.right) {
			clonePosition = entity.transform.position + new Vector3(xPos,yPos,0);
			cloneVelocity = new Vector3 (projectileSpeed, angle, 0);
			cloneOrientation = Quaternion.Euler(0, 0, rotation);
		}
		Utilities.cloneObject(bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		Physics2D.IgnoreCollision (bulletToClone.collider2D, entity.collider2D);
		Destroy (bulletToClone,projectileDuration);
	}
}
