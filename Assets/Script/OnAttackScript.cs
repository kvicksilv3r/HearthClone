using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnAttackScript : MonoBehaviour
{

	Creature parent;
	GameManager gameManager;

	public void Attack()
	{
		parent = transform.parent.GetComponent<Creature>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

		foreach (int ability in parent.Abilities)
		{
			if (ability == 10)
			{
				if (gameManager.TimeIndex == 2)
				{
					gameManager.HeroDamage(parent.OwnerId, -gameManager.LastDamageTaken(Mathf.Abs(parent.OwnerId + 1 - 2)));
				}
			}

			if (ability == 30)
			{
				print("GDFGDG");
				if (gameManager.GetLastAttackedCreature(Mathf.Abs(parent.OwnerId + 1 - 2)).IsDead == false)
				{
					print("Hellå ja");
					gameManager.GetLastAttackedCreature(Mathf.Abs(parent.OwnerId + 1 - 2)).StartCoroutine("Death", true);
				}
			}
		}
	}
}
