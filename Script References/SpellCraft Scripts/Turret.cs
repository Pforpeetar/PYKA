using UnityEngine;
using System.Collections;

public class Turret : Switchable {
	public GameObject refBullet;
	public Direction direction;
	public int projectileSpeed = 20;
	public const float COOLDOWNTIME = .15f;
	private float timeSinceLastFired = 0f;
	private LayerMask pMask = 1 << 9;
	public bool isActive;
	private Vector3 p;
	private Vector3 s; //box collider size to help with raycasting
	private Vector3 c; //box collider center to help with raycasting
	// Use this for initialization

	void Start () {
		refBullet = GameObject.FindGameObjectWithTag("EnemyProjectile");
			BoxCollider2D zollider = GetComponent<BoxCollider2D> (); //get attached collider, store size and center
			s = zollider.size;
			c = zollider.offset;
			p = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	if ((isActive) && (inRange()) && (canFire()))
		{
			fireBullet();
		}
	}

	private bool inRange()
	{
		//all needed to raycast
		int xAxisDir = 0;
		int yAxisDir = 0;
		if (direction == Direction.up) {
			yAxisDir = 1;		
		} else if (direction == Direction.down) {
			yAxisDir = -1;
		} else if (direction == Direction.right) {
			xAxisDir = 1;
		} else if (direction == Direction.left) {
			xAxisDir = -1;
		}
		Vector3 castDirection = new Vector3 (xAxisDir, yAxisDir);
			for (int i = 0; i < 3; i++) {
				float x = (p.x + c.x + (s.x / 2)) - (s.x / 2 * i * (yAxisDir * yAxisDir)) + ((s.x / 2 * (xAxisDir - 1)) * (xAxisDir * xAxisDir));
				float y = (p.y + c.y + (s.y / 2)) - (s.y / 2 * i * (xAxisDir * xAxisDir)) + ((s.y / 2 * (yAxisDir - 1)) * (yAxisDir * yAxisDir));
				Ray ray = new Ray (new Vector3 (x, y, 0), castDirection);
				Debug.DrawRay(ray.origin,ray.direction);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction,15,pMask);
				if (hit && hit.collider && ((Utilities.hasMatchingTag("Player",hit.collider.gameObject)))) {
					return true;
				}
			}
				return false;
	}
	//used kind of like a cooldown because shooting a bullet every frame is excessive, trust me
	private bool canFire()
	{
		if (timeSinceLastFired > COOLDOWNTIME) 
		{
			timeSinceLastFired = 0f;
			return true;
		}
		else
		{
			timeSinceLastFired += Time.deltaTime;
			return false;
		}
	}

	private void fireBullet()
	{
		if (refBullet == null) return;
		Vector3 clonePosition = new Vector3(0,0,0) ;
		Vector3 cloneVelocity = new Vector3(0,0,0);
		Quaternion cloneOrientation = Quaternion.Euler(0,0,0); 
		if (direction == Direction.down) {
			clonePosition = transform.position + new Vector3(0,-1,0);
			cloneVelocity = new Vector3 (0, -projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 270);
		}
		else if (direction == Direction.up) {
			clonePosition = transform.position + new Vector3(0,1,0);
			cloneVelocity = new Vector3 (0, projectileSpeed, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 90);
		}
		else if (direction == Direction.left) {
			clonePosition = transform.position + new Vector3(-1, 0, 0);
			cloneVelocity = new Vector3 (-projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 180);
		}
		else if (direction == Direction.right) {
			clonePosition = transform.position + new Vector3(1,0,0);
			cloneVelocity = new Vector3 (projectileSpeed, 0, 0);
			cloneOrientation = Quaternion.Euler(0, 0, 0);
		}
		//GameObject clonedesu = createSpellObject(direction, bulletToClone, clonePosition, cloneVelocity, cloneOrientation);
		GameObject clonedesu = Utilities.cloneObject(direction, refBullet, clonePosition, cloneVelocity, cloneOrientation);
		//Debug.Log(clonedesu.transform.position);
		Physics2D.IgnoreCollision (clonedesu.GetComponent<Collider2D>(), GetComponent<Collider2D>());
		Destroy (clonedesu,1);
	}

	public override void flipStatus()
	{
		isActive = !isActive;
		updateStatus();
	}

	/** 
	 * called by a pressure plate that can have multiple triggers
	 * If you want the door to be locked,   pass in false
	 * If you want the door to be unlocked, pass in true
	 */
	public override void setStatus(bool activeStatus)
	{
		isActive = !activeStatus; 
		updateStatus();
	}
	/**
	 * If the door is locked,  reenable it's collider and update the animation
	 * If the door is unlocked, disable it's collider and update the animation
	 */ 
	void updateStatus()
	{
		//animator.SetBool ("isActive", isLocked); 
	}
}
