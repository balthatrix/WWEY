using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathEffect : MonoBehaviour {
	public Damageable damageable;


	public bool destroysOnDeath = true;
	public float destroyDelay = .3f;
	// Use this for initialization
	void Start () {
		damageable.OnDied += (Damageable self) => {
			if(deathParticle != null) {
				GameObject p = Instantiate(deathParticle);
				p.transform.position = transform.position;

				DeathGhostDone eff = p.GetComponent<DeathGhostDone>();
				if(eff != null) {
					eff.toFollow = transform;
				}
			}

			if(deathSound != null) {
				deathSound.Play();
			}
			StartCoroutine(DelayDestroy());


		};
	}

	IEnumerator DelayDestroy() {
		yield return new WaitForSeconds (destroyDelay);

		Destroy(gameObject);
		if (toSpawnOnDestroy != null) {
			GameObject go = Instantiate (toSpawnOnDestroy);
			go.transform.position = transform.position;
		}
	}


	public GameObject deathParticle;
	public AudioSource deathSound;

	public GameObject toSpawnOnDestroy;

}
