using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour {

	[SerializeField]
	private int maxHealth = 1;
	[SerializeField]
	private int health;


	void Start() {
		health = maxHealth;
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

	public void TakeDamage(int amount) {
		health -= amount;
		if (OnDamaged != null) {
			OnDamaged (this, amount);	
		}

		if (health <= 0) {
			if (OnDied != null) {
				OnDied (this);
			}	
		}
	}
}
