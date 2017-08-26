using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderEnableTrigger : MonoBehaviour {


	void Start() {
		DisableCollider ();
	}

	public void EnableCollider() {
		GetComponent<Collider2D> ().enabled = true;

	}

	public void DisableCollider() {
		GetComponent<Collider2D> ().enabled = false;
	}

	public void Loop() {
		if (OnLooped != null) {
			OnLooped ();
		}
	} 

	public delegate void LoopedAction();
	public event LoopedAction OnLooped;
}
