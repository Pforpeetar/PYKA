using UnityEngine;
using System.Collections;

public class BossLimb : Entity {

	public override void deathCheck() {
		if (!deathState) { //check to see if they are already in death state
			if (health <= 0) {
				transform.parent.GetComponent<Entity>().damageEntity(1000);
				deathState = true;
				Utilities.score += scoreBounty;
				if (gameObject.CompareTag("Enemy")) {
					Utilities.enemyCount -= 1;
					if (Random.Range(0, 100) < dropRate && healthDrop != null) {
						Instantiate(healthDrop, transform.position, Quaternion.identity);
					}
				}

				gameObject.SetActive(false);
				//gameObject.collider2D.enabled = false;
			}
		}
	}
}
