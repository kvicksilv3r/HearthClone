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

            CheckTarget();

            if (aiCreature.CurrentAttacks >= 1 && aiCreature.CanAttack && attackTarget != null)
            {
                if (aiCreature.CurrentAttacks > 1 && playerCreature.Health > 0)
                {
                    playerCreature.TakeDamage(aiCreature.Strength);
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
                    
                    CheckTarget();

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

        yield return new WaitForSeconds(waitTime);

        gameManager.NextRound(false);

        yield return null;
    }

    public void CheckTarget()
    {
        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            playerCreature = target.transform.GetChild(0).GetComponent<Creature>();

            if (aiCreature.Strength >= playerCreature.Health && target.gameObject.transform.GetChild(0).GetComponent<Creature>().HasTaunt)
            {
                attackTarget = target.gameObject;
            }
            else if (target.gameObject.transform.GetChild(0).GetComponent<Creature>().HasTaunt)
            {
                attackTarget = target.gameObject;
            }
            else if (aiCreature.Strength >= playerCreature.Health)
            {
                attackTarget = target.gameObject;
            }
            else
            {
                attackTarget = TargetList[Random.Range(0, TargetList.Count)];
            }
        }
    }
}
