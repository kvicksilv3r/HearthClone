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
            else if (aiCreature.CanAttack && aiCreature.CurrentAttacks > 1 && GameObject.Find("Player Playfield").transform.childCount < 3)
            {
                gameManager.HeroDamage(0, aiCreature.Strength);
                yield return new WaitForSeconds(waitTime);

                aiCreature.CurrentAttacks--;
            }
            else if (aiCreature.CanAttack && aiCreature.CurrentAttacks > 0 && GameObject.Find("Player Playfield").transform.childCount < 3)
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

        if (GameObject.Find("Player Playfield").transform.childCount <= 0)
        {
            return false;
        }

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            playerCreature = target.transform.GetChild(0).GetComponent<Creature>();


            if (aiCreature.Strength >= playerCreature.Health && playerCreature.HasTaunt)
            {
                attackTarget = target.gameObject;
                return true;
            }
            else if (playerCreature.HasTaunt)
            {
                attackTarget = target.gameObject;
                return true;
            }
            else if (aiCreature.Strength >= playerCreature.Health && aiCreature.Health > playerCreature.Strength)
            {
                attackTarget = target.gameObject;
                returnBool = true;
            }
            else if(aiCreature.CanAttack && aiCreature.CurrentAttacks > 0)
            {
                attackTarget = TargetList[Random.Range(0, TargetList.Count + 1)].gameObject;
            }
        }
        return returnBool;
    }
}
