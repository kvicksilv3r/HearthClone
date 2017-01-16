using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTaunt : MonoBehaviour {

	

	// Use this for initialization
	void Start () {
		
	}
	
	public void PlayTaunt()
	{
		GetComponent<AudioSource>().clip = Resources.Load("WarlockTaunt" + Random.Range(1,4).ToString()) as AudioClip;
		GetComponent<AudioSource>().Play();
    }
}
