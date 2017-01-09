using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableText : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisableTexts()
	{
		foreach(Text t in gameObject.GetComponentsInChildren<Text>())
		{
			t.enabled = false;
		}
	}
}
