using UnityEngine;
using System.Collections;

public abstract class ProjectileMovement : MonoBehaviour {
	public GameObject bullet; //bullet prefab, drag onto script
	public int speed = 10; //default projectile speed
	public float lifeSpan = 1;
	public float shotCooldown = .1f;
	public float hitTime;
	public abstract void movement ();
}
