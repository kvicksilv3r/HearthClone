using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
    //GameObject[] targets;

    List<GameObject> TargetList;
    GameObject attackTarget;
    Creature aiCreature;
    Creature playerCreature;
    GameManager gameManager;
	List<Creature> killabeTaunt = new List<Creature>();
	List<Creature> tauntCreatures = new List<Creature>();
	List<Creature> killableCreatures = new List<Creature>();
	List<Creature> rest = new List<Creature>();
	float waitTime = 2;

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

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            TargetList.Add(target.gameObject);
        }

        foreach (Transform child in transform)
        {
            aiCreature = child.GetChild(0).GetComponent<Creature>();

            if (CheckCreature())
            {
                if (aiCreature.CurrentAttacks >= 1 && aiCreature.CanAttack && attackTarget != null)
                {
                    if (aiCreature.CurrentAttacks > 1 && playerCreature.Health > 0)
                    {
                        attackTarget.transform.GetChild(0).GetComponent<Creature>().TakeDamage(aiCreature.Strength);
                        aiCreature.TakeDamage(playerCreature.Strength);

                        Debug.Log("First target hit");

                        aiCreature.CurrentAttacks--;

                        if (playerCreature.Health < 1)
                        {
                            Debug.Log("Target killed on first attack");

                            attackTarget = null;
                        }
                    }

                    if (attackTarget == null)
                    {
                        Debug.Log("New target");

                        //recheck here yo

                        if (attackTarget == null)
                        {
                            Debug.Log("no targets");
                            break;
                        }
                    }

                    yield return new WaitForSeconds(waitTime);

                    if (aiCreature.Health > 0 && playerCreature.Health > 0)
                    {
                        playerCreature.TakeDamage(aiCreature.Strength);
                        aiCreature.TakeDamage(playerCreature.Strength);

                        Debug.Log("Second Attack");

                        aiCreature.CurrentAttacks--;

                        yield return new WaitForSeconds(waitTime);
                    }
                }
            }
            else if (aiCreature.CanAttack && aiCreature.CurrentAttacks > 1)
            {
                gameManager.HeroDamage(0, aiCreature.Strength);
                yield return new WaitForSeconds(waitTime);

                aiCreature.CurrentAttacks--;
            }
            else if (aiCreature.CanAttack && aiCreature.CurrentAttacks > 0)
            {
                gameManager.HeroDamage(0, aiCreature.Strength);
                yield return new WaitForSeconds(waitTime);

                aiCreature.CurrentAttacks--;
            }
        }

        yield return new WaitForSeconds(waitTime);

        gameManager.NextRound(false);

        yield return null;
    }

    public bool CheckCreature()
    {
        bool returnBool = false;

		killabeTaunt.Clear();
		tauntCreatures.Clear();
		killableCreatures.Clear();
		rest.Clear();

        if (GameObject.Find("Player Playfield").transform.childCount <= 0)
        {
            return false;
        }

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            playerCreature = target.transform.GetChild(0).GetComponent<Creature>();

            if (aiCreature.Strength >= playerCreature.Health && playerCreature.GetComponent<Creature>())
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
            else if(aiCreature.CanAttack && aiCreature.CurrentAttacks > 0)
            {
				rest.Add(playerCreature);
				continue;
            }
        }
        
		if(killabeTaunt.Count > 0)
		{
			attackTarget = killabeTaunt[Random.Range(0, killabeTaunt.Count)].gameObject;
			return true;
		}
		else if(tauntCreatures.Count > 0)
		{
			attackTarget = tauntCreatures[Random.Range(0, tauntCreatures.Count)].gameObject;
			return true;
		}
		else if (killableCreatures.Count > 0)
		{
			attackTarget = killableCreatures[Random.Range(0, killableCreatures.Count)].gameObject;
			return true;
		}
		else if(rest.Count > 0)
		{
			if(Random.Range(0,3) != 0)
			{
				attackTarget = rest[Random.Range(0, rest.Count)].gameObject;
				returnBool = true;
			}
			else
			{
				returnBool = false;
			}
		}
		else
		{
			returnBool = false;
		}

		return returnBool;
    }
}
