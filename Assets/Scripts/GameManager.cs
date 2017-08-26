using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Editor-Visible Fields
	public static GameManager instance;
	[SerializeField]
	private GameObject heroPrefab;
	[SerializeField]
	private SavePoint firstSave;

	// Fields
	private SavePoint currentSave;

	// Methods
	public void SetSave(SavePoint save) {
		currentSave = save;
		save.DecorateAsCurrentSave ();
	}

	public void SpawnHero() {
		GameObject hero = Instantiate (heroPrefab);
		hero.transform.position = currentSave.transform.position;
	}

	// Mono-Behavior Methods
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	void Start () {
		SetSave (firstSave);
		SpawnHero ();
	}
}
