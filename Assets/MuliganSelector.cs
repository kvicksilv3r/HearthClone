using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuliganSelector : MonoBehaviour {

	public int id;
	bool visible;
	[SerializeField]
	MeshRenderer mRender;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		visible = !visible;
		transform.parent.parent.GetComponent<MuliganScript>().ChoseDiscard(id);
		mRender.enabled = visible;
	}
}
