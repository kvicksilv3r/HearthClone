using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
	//GameObject[] targets;

	List<GameObject> TargetList;
	Creature attackTarget;
	Creature aiCreature;
	Creature playerCreature;
	GameManager gameManager;
	List<Creature> killabeTaunt = new List<Creature>();
	List<Creature> tauntCreatures = new List<Creature>();
	List<Creature> killableCreatures = new List<Creature>();
	List<Creature> rest = new List<Creature>();
	float waitTime = 2;
	bool goFace = false;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		TargetList = new List<GameObject>(GameObject.Find("Player Playfield").transform.childCount);
	}

	public void AIBattlePhase()
	{
		StartCoroutine("BattlePhase");
	}

	IEnumerator BattlePhase()
	{
		TargetList.Clear();

		goFace = CanKillHero();

		foreach (Transform target in GameObject.Find("Player Playfield").transform)
		{
			TargetList.Add(target.gameObject);
		}

		foreach (Creature creature in transform.GetComponentsInChildren<Creature>())
		{
			aiCreature = creature;
			while (aiCreature.CurrentAttacks >= 1 && aiCreature.CanAttack)
			{
				if (CheckCreature())
				{
					if (attackTarget != null)
					{
						yield return new WaitForSeconds(waitTime);
						attackTarget.TakeDamage(aiCreature.Strength);
						aiCreature.TakeDamage(attackTarget.Strength);
						aiCreature.CurrentAttacks--;
						
					}
					else
					{
						yield return new WaitForSeconds(waitTime);
						AttackHero(aiCreature.Strength);
					}
				}
				else
				{
					yield return new WaitForSeconds(waitTime);
					AttackHero(aiCreature.Strength);
				}
			}
		}

		yield return new WaitForSeconds(waitTime);

		gameManager.NextRound(false);

		yield return null;
	}

	void AttackHero(int dmg)
	{
		gameManager.HeroDamage(0, dmg);
		aiCreature.CurrentAttacks--;
	}

	bool CanKillHero()
	{
		int dmg = 0;

		foreach (Creature cr in GameObject.Find("Player Playfield").transform.GetComponentsInChildren<Creature>())
		{
			if (cr.HasTaunt)
			{
				return false;
			}
		}

		foreach (Creature cr in transform.GetComponentsInChildren<Creature>())
		{
			if (cr.CanAttack && cr.CurrentAttacks > 0)
				dmg += cr.Strength * cr.CurrentAttacks;
		}

		if (dmg > gameManager.Players()[0].health)
		{
			return true;
		}

		else
		{
			return false;
		}
	}

	public bool CheckCreature()
	{
		killabeTaunt.Clear();
		tauntCreatures.Clear();
		killableCreatures.Clear();
		rest.Clear();

		if (GameObject.Find("Player Playfield").transform.childCount <= 0)
		{
			return false;
		}

		foreach (Creature creature in GameObject.Find("Player Playfield").transform.GetComponentsInChildren<Creature>())
		{
			playerCreature = creature;

			if (aiCreature.Strength >= playerCreature.Health && playerCreature.HasTaunt)
			{
				killabeTaunt.Add(playerCreature);
				continue;
			}
			else if (playerCreature.HasTaunt)
			{
				tauntCreatures.Add(playerCreature);
				continue;
			}
			else if (aiCreature.Strength >= playerCreature.Health && aiCreature.Health > playerCreature.Strength)
			{
				killableCreatures.Add(playerCreature);
				continue;
			}
			else if (aiCreature.CanAttack && aiCreature.CurrentAttacks > 0)
			{
				rest.Add(playerCreature);
				continue;
			}
		}

		if (killabeTaunt.Count > 0)
		{
			attackTarget = killabeTaunt[Random.Range(0, killabeTaunt.Count)];
			return true;
		}
		else if (tauntCreatures.Count > 0)
		{
			attackTarget = tauntCreatures[Random.Range(0, tauntCreatures.Count)];
			return true;
		}
		else if (killableCreatures.Count > 0)
		{
			attackTarget = killableCreatures[Random.Range(0, killableCreatures.Count)];
			return true;
		}
		else if (rest.Count > 0)
		{
			if (Random.Range(0, 3) != 0)
			{
				attackTarget = rest[Random.Range(0, rest.Count)];
				return true;
			}
			else
			{
				return false;
			}
		}
		else
		{
			return false;
		}

		return false;
	}
}
