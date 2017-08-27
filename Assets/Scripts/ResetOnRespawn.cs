using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetOnRespawn : MonoBehaviour {

	[SerializeField]
	private EnemyID eyeDee;
	private Vector3 startPosition;
	private bool notKilled = false;

	public enum EnemyID {
		TREE,
		ROCK,
		SAVE
	}

	void Reset(Hero h){
		GameObject reset = null;

		switch (eyeDee) { // THIS IS NOT DRY, YOU MUST MAKE CHANGES BELOW AS WELL AS HERE
		case (EnemyID.ROCK):
			reset = Instantiate (GameManager.instance.rockEnemyFab);
			break;
		case (EnemyID.TREE):
			reset = Instantiate (GameManager.instance.treeEnemyFab);
			break;
		case (EnemyID.SAVE):
			reset = Instantiate (GameManager.instance.saveEnemyFab);
			break;
		}

		if (reset != null) {
			reset.transform.position = startPosition;
			notKilled = true;
			Destroy (this.gameObject);
		}
	}

	// Use this for initialization
	void Start () {
		startPosition = transform.position;
		GameManager.instance.OnHeroRespawn += Reset;
	}

	void OnDestroy () {
		if (!notKilled) {
			DeadGuyToRespawn me = new DeadGuyToRespawn (startPosition, eyeDee);
			GameManager.instance.respawnList.Add (me);
		}
		GameManager.instance.OnHeroRespawn -= Reset;
	}

	public class DeadGuyToRespawn {

		private Vector3 spawnHere;
		private EnemyID iidd;

		public DeadGuyToRespawn (Vector3 pos, EnemyID id) {
			iidd = id;
			spawnHere = pos;
		}

		public void Respawn(){
			GameObject reset = null;

			switch (iidd) {
			case (EnemyID.ROCK):
				reset = Instantiate (GameManager.instance.rockEnemyFab);
				break;
			case (EnemyID.TREE):
				reset = Instantiate (GameManager.instance.treeEnemyFab);
				break;
			case (EnemyID.SAVE):
				reset = Instantiate (GameManager.instance.saveEnemyFab);
				break;
			}

			if (reset != null) {
				reset.transform.position = spawnHere;
			}
		}
	}
}
