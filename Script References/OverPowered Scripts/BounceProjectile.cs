using UnityEngine;
using System.Collections;

public class BounceProjectile : BulletDesu {
	public int projectileSpeed=0; //for spell that shoot something out
	protected GameObject entity;
	protected EntityStateManager entityStates;
	private Vector3 clonePosition;
	private Vector3 cloneVelocity;
	private Quaternion cloneOrientation;
	private Direction direction;
	private GameObject bulletToClone;

	public void setState(EntityStateManager e) {
		entityStates = e;
	}

	public override void execute(GameObject g) {
		entity = g;
		direction = entityStates.GetDirState ();
		bulletToClone = gameObject;
		if (entity == null){entity = GameObject.FindGameObjectWithTag("Entity");}
		createProjectile (0.5f, 0.5f, 0, 3);
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
		Destroy (bulletToClone, projectileDuration);
	}
}
