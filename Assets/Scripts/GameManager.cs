﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Editor-Visible Fields
	public static GameManager instance;
	[SerializeField]
	private GameObject heroPrefab;
	[SerializeField]
	private GameObject tutorialPrefab;
	[SerializeField]
	private SavePoint firstSave;

<<<<<<< HEAD
=======
	[SerializeField]
	private List<SavePoint> allSavePoints;
>>>>>>> f0f8e6cf7eb67a7325fb294e90333f812b288e9d

	// Fields
	private SavePoint currentSave;
	private TutorialText currentText;

	// Methods
	public void SetSave(SavePoint save) {
		currentSave = save;
		save.DecorateAsCurrentSave ();
	}

	public void SpawnHero() {
		GameObject hero = Instantiate (heroPrefab);
		hero.transform.position = currentSave.transform.position;
	}

	public void SpawnText(string whatToSay) {
		GameObject text = Instantiate (tutorialPrefab);
		if (currentText != null) {
			currentText.CleanupText ();
		}
		currentText = text.GetComponent<TutorialText> ();
		currentText.Say (whatToSay);
	}

	// Mono-Behavior Methods
	void Awake () {
		if (instance == null) {
			instance = this;
		} else {
			Destroy (this);
		}
	}

	IEnumerator Start () {
		//Give time for all save points to check in.
		yield return new WaitForEndOfFrame ();
		SetSave (firstSave);
		SpawnHero ();
		SpawnText ("SCREAMING SCREAMING");
	}

	public void CheckInSavePoint(SavePoint p) {
		if (p.isFirst) {
			firstSave = p;
		}
		allSavePoints.Add (p);
	}


}
