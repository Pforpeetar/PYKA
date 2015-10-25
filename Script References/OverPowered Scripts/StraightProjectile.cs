using UnityEngine;
using System.Collections;
using System;

public class StraightProjectile : BulletDesu {
	public int projectileSpeed=0; //for spell that shoot something out
	protected GameObject entity;
	protected EntityStateManager entityStates;
	private Vector3 clonePosition;
	private Vector3 cloneVelocity;
	private Quaternion cloneOrientation;
	private Direction direction;
	private GameObject bulletToClone;
	public float shotAngle;
	private double shotAngleR;

	public void setState(EntityStateManager e) {
		entityStates = e;
	}

	public override void execute(GameObject g) {
		entity = g;
		direction = entityStates.GetDirState ();
		bulletToClone = gameObject;
		if (entity == null){entity = GameObject.FindGameObjectWithTag("Entity");}
		shotAngleR = ((Math.PI / 180) * shotAngle); //allows you to set in inspector as degrees
		createProjectile ((float)Math.Round(1f * Math.Cos(shotAngleR)), (float)Math.Round(1f * Math.Sin(shotAngleR)), 0, shotAngleR);
	}

	public void createProjectile(float xPos, float yPos, float rotation, double angle) {
			if (direction == Direction.left) {
			clonePosition = entity.transform.position + new Vector3(-xPos, yPos, 0);
			cloneVelocity = new Vector3 (-projectileSpeed * (float)Math.Round(Math.Cos(angle),2), projectileSpeed * (float)Math.Round(Math.Sin(angle),2), 0);
			cloneOrientation = Quaternion.Euler(0, 0, rotation + 180 - shotAngle);
		}
		else if (direction == Direction.right) {
			clonePosition = entity.transform.position + new Vector3(xPos,yPos,0);
			cloneVelocity = new Vector3 (projectileSpeed * (float)Math.Round(Math.Cos(angle),2), projectileSpeed * (float)Math.Round(Math.Sin(angle),2), 0);
			cloneOrientation = Quaternion.Euler(0, 0, rotation + shotAngle);
		}
		Utilities.cloneObject(bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		Physics2D.IgnoreCollision (bulletToClone.collider2D, entity.collider2D);
		Destroy (bulletToClone,projectileDuration);
	}
}
