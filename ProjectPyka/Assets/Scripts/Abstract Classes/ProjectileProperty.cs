using UnityEngine;
using System.Collections;

public abstract class ProjectileProperty : MonoBehaviour {
	public EntityType target;
	protected Entity e;
	public abstract void property();
}
