﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvilTreeBehavior : MonoBehaviour {
	public AggroMimic mimic;

	public TriggerListener attackTrigger;
	public Animator attackAnimation;
	public float cooldown = 1f;
	public Hero toAttack;

	// Use this for initialization
	void Start () {
		mimic.OnGotUp += () =>  {
			GetComponent<Animator>().SetTrigger("Seeking");
		};
		mimic.OnSatDown += () => {
			GetComponent<Animator>().SetTrigger("Idle");
		};


		attackTrigger.OnTriggerEntered += (Collider2D other) => {
			Hero h = other.GetComponent<Hero>();
			if(h != null) {
				toAttack = h;
				StartCoroutine(KeepAttacking());
			}
		};
		attackTrigger.OnTriggerExited += (Collider2D other) => {
			Hero h = other.GetComponent<Hero>();
			if(h != null) {
				toAttack = null;
			}
		};
	}


	void TryAttack() {
		if (cooling) {
			return;
		}
		attackAnimation.gameObject.SetActive (true);
		attackAnimation.GetComponent<ColliderEnableTrigger> ().OnLooped += TurnOffWhenLooped;
		StartCoroutine (DoCooldown ());
	}

	void TurnOffWhenLooped() {
		attackAnimation.gameObject.SetActive (false);
		attackAnimation.GetComponent<ColliderEnableTrigger> ().OnLooped -= TurnOffWhenLooped;

	}

	IEnumerator KeepAttacking() {
		while (toAttack != null) {
			TryAttack ();
			yield return new WaitForSeconds (.1f);
		}
	}


	bool cooling = false;
	IEnumerator DoCooldown() {
		cooling = true;
		yield return new WaitForSeconds (cooldown);	
		cooling = false;
	}
}