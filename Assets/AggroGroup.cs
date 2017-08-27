using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AggroGroup : MonoBehaviour {

	public List<AggroMimic> aggrodMimics = new List<AggroMimic> ();

	public AggroMimic[] mimicGroup;

	// Use this for initialization
	void Start () {
		foreach(AggroMimic m in mimicGroup) {
			m.aggGroup = this;
			m.OnGotUp += TrackMimic;
			m.OnSatDown += UntrackMimic;
		}	
	}


	void TrackMimic(AggroMimic mimic) {
		aggrodMimics.Add (mimic);
	}

	void UntrackMimic(AggroMimic mimic) {
		aggrodMimics.Remove (mimic);
	}

	//for if they die.
	void CleanMimics() {
		aggrodMimics = aggrodMimics.Where (a => a != null).ToList();
	}

	public Hero HeroToChase() {
		CleanMimics ();
		if (aggrodMimics.Count > 0) {
			return aggrodMimics.Last ().heroToChase;
		} else {
			return null;
		}
	} 

}
