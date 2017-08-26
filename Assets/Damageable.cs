using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	[SerializeField]
	private int maxHealth = 1;
	[SerializeField]
	private int health;


	public Rigidbody2D pushbackRigidbody;

	public GameObject thingWithMovement;
	private HasMovement movementToLock;

	void Start() {
		health = maxHealth;
		if (thingWithMovement != null) {
			movementToLock = thingWithMovement.GetComponent<HasMovement> ();
		}
	}
	public int Health {
		get {
			return health;
		}
	}

	public delegate void DamagedAction(Damageable self, int amount);
	public event DamagedAction OnDamaged;

	public delegate void DiedAction(Damageable self);
	public event DiedAction OnDied;

	public void TakeDamage(Damager source, int amount, float knockbackMag) {
		health -= amount;
		if (OnDamaged != null) {
			OnDamaged (this, amount);	
		}

		Vector3 away = (transform.position - source.transform.position).normalized * knockbackMag;


		pushbackRigidbody.AddForce (away, ForceMode2D.Impulse);

		if (thingWithMovement != null) {
			movementToLock.LockMovement ();
			StartCoroutine (UnlockAfterStun ());
		}

		if (health <= 0) {
			if (OnDied != null) {
				OnDied (this);
			}	
		}
	}

	float unlockThreshold = .2f;

	IEnumerator UnlockAfterStun() {
		while (pushbackRigidbody.velocity.sqrMagnitude > unlockThreshold) {
			yield return null;
		}
		movementToLock.UnlockMovement ();
	}
}
