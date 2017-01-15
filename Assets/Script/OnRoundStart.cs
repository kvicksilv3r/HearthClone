using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoundStart : MonoBehaviour {

	Creature parent;
	GameManager gameManager;

	public void RoundStart()
	{
		parent = transform.parent.GetComponent<Creature>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		foreach(int ability in parent.Abilities)
		{
			if(ability == 8)
			{
				parent.TakeDamage(1);
				gameManager.HeroDamage(parent.OwnerId, 1);
			}


		}


	}
}
