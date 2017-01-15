using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathRattleScript : MonoBehaviour
{

	Creature parent;
	GameManager gameManager;


	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void DeathRattle()
	{
		parent = transform.parent.GetComponent<Creature>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();


		if (parent.Card.card_id == 18)
		{
			print("should heal");
			gameManager.HeroDamage(parent.OwnerId, -2);
		}

	}
}
