﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeHiderScript : MonoBehaviour {

	bool active = false;
	public GameObject particleHolder;
    protected float yPos;
    protected float zPos;


	// Use this for initialization
	void Start () {

		particleHolder = GameObject.Find("ParticleHolder");
        yPos = transform.position.y;
        zPos = transform.position.z;
		
	}
	
	// Update is called once per frame
	void Update () {

		if (active)
		{
			transform.position = new Vector3(transform.position.x + ((75 / 20f) * Time.deltaTime), yPos, zPos);
			if (transform.position.x > 0)
			{
				active = false;
                transform.position = new Vector3(0, yPos, zPos);
				StartCoroutine(RemoveFire(3));
			}
		}

	}

	public void Activate()
	{
		active = true;
		transform.position = new Vector3(-75, yPos, zPos);
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