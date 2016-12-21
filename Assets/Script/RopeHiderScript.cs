using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHiderScript : MonoBehaviour {

	bool active = false;
	public GameObject particleHolder;

	// Use this for initialization
	void Start () {

		particleHolder = GameObject.Find("ParticleHolder");
		
	}
	
	// Update is called once per frame
	void Update () {

		if (active)
		{
			transform.position = new Vector3(transform.position.x + ((75 / 20f) * Time.deltaTime), 0, 0);
			if (transform.position.x > 0)
			{
				active = false;
				transform.position = Vector3.zero;
				StartCoroutine(RemoveFire(3));
			}
		}

	}

	public void Activate()
	{
		active = true;
		transform.position = new Vector3(-75, 0, 0);
		particleHolder.SetActive(true);

		foreach(ParticleSystem ps in GetComponentsInChildren<ParticleSystem>())
		{
			ps.Play();
		}
	}

	public void Deactivate()
	{
		StartCoroutine(RemoveFire(0));
	}

	IEnumerator RemoveFire(int killTime)
	{
		yield return new WaitForSeconds(killTime);
		particleHolder.SetActive(false);
	}
}
