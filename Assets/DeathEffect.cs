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
			}

			if(deathSound != null) {
				deathSound.Play();
			}
			Destroy(gameObject, destroyDelay);
		};
	}


	public GameObject deathParticle;
	public AudioSource deathSound;
}
