using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnRoundStart : MonoBehaviour
{

	Creature parent;
	GameManager gameManager;

	public void RoundStart()
	{
		parent = transform.parent.GetComponent<Creature>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		if (parent.IsDead == false && gameManager.PlayerTurn == parent.OwnerId)
		{
			print("Stuff should happen");
			foreach (int ability in parent.Abilities)
			{
				if (ability == 8) //Evil meda
				{
					print("Evil Meda");
					parent.TakeDamage(1);
					gameManager.HeroDamage(parent.OwnerId, 1);
				}

				else if (ability == 21) //maiden of favor
				{
					print("Hello world");
					gameManager.Boards[parent.OwnerId].transform.GetComponentsInChildren<Creature>()[Random.Range(0, gameManager.Boards[parent.OwnerId].transform.GetComponentsInChildren<Creature>().Length)].Strength += 2;
					gameManager.Boards[parent.OwnerId].transform.GetComponentsInChildren<Creature>()[Random.Range(0, gameManager.Boards[parent.OwnerId].transform.GetComponentsInChildren<Creature>().Length)].Health += 1;

				}
			}
		}
	}
}
