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

		foreach (int ability in parent.Abilities)
		{
			if (ability == 13) //old seer
			{
				foreach (Creature cr in gameManager.Boards[parent.OwnerId].transform.GetComponentsInChildren<Creature>())
				{
					cr.Strength -= 2;
					cr.Health -= 1;
				}
			}

			else if (ability == 15) //Young martyr
			{
				gameManager.HeroDamage(parent.OwnerId, -3);
			}
		}

	}
}
