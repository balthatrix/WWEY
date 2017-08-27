using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextTrigger : MonoBehaviour {

	[SerializeField]
	[TextArea(5, 5)]
	private string text;
	private bool triggered;

	void Reset (Hero notUsed) {
		triggered = false;
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.GetComponent<Hero>() != null && !triggered) {
			GameManager.instance.SpawnText (text);
			triggered = true;
		}
	}

	void Start () {
		GameManager.instance.OnHeroRespawn += Reset;
	}

	void OnDestroy() {
		GameManager.instance.OnHeroRespawn -= Reset;
	}
}
