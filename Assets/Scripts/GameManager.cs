using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	// Editor-Visible Fields
	public static GameManager instance;
	public GameObject rockEnemyFab;
	public GameObject treeEnemyFab;
	public GameObject saveEnemyFab;
	[SerializeField]
	private GameObject heroPrefab;
	[SerializeField]
	private GameObject tutorialPrefab;
	[SerializeField]
	private GameObject startScreen;
	[SerializeField]
	private GameObject deathScreen;
	[SerializeField]
	private SavePoint firstSave;
	[SerializeField]
	private List<SavePoint> allSavePoints;

	// Fields
	private SavePoint currentSave;
	private Damageable currentHeroDamageable;
	private TutorialText currentText;

	// Events
	public delegate void HeroRespawnAction();
	public event HeroRespawnAction OnHeroRespawn;

	// Methods
	public void SetSave(SavePoint save) {
		if (currentSave != save) {
			if (currentSave != null) {
				currentSave.DecorateAsNotCurrentSave ();
			}
			currentSave = save;
			currentSave.DecorateAsCurrentSave ();
		}
	}

	public void SpawnHero() {
		if (OnHeroRespawn != null) {
			OnHeroRespawn ();
		}

		if (startScreen.activeInHierarchy) {
			startScreen.SetActive (false);
		}

		if (deathScreen.activeInHierarchy) {
			deathScreen.SetActive (false);
		}

		GameObject hero = Instantiate (heroPrefab);
		hero.transform.position = currentSave.transform.position;

		currentHeroDamageable = hero.GetComponent<Damageable>();
		currentHeroDamageable.OnDied += SetupDeathScreen;
	}

	public void SetupDeathScreen(Damageable dmgble) {
		currentHeroDamageable.OnDied -= SetupDeathScreen;
		StartCoroutine (DeathScreenRoutine());
	}

	private IEnumerator DeathScreenRoutine() {
		yield return new WaitForSeconds (0.25f);
		deathScreen.SetActive (true);
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

		deathScreen.SetActive (false);
	}

	IEnumerator Start () {
		//Give time for all save points to check in.
		yield return new WaitForEndOfFrame ();
		SetSave (firstSave);
	}

	public void CheckInSavePoint(SavePoint p) {
		if (p.isFirst) {
			firstSave = p;
		}
		allSavePoints.Add (p);
	}


}
