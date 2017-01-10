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

    void Start()
    {
        TargetList = new List<GameObject>(GameObject.Find("Player Playfield").transform.childCount);
    }

    public void AIBattlePhase()
    {
        TargetList.Clear();

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            TargetList.Add(target.gameObject);

            if (target.gameObject.transform.GetChild(0).GetComponent<Creature>().HasTaunt)
            {
                attackTarget = target.gameObject;
            }
            else
            {
                attackTarget = TargetList[Random.Range(0, TargetList.Count)];
            }
            playerCreature = attackTarget.transform.GetChild(0).GetComponent<Creature>();
        }

        foreach (Transform child in transform)
        {
            aiCreature = child.GetChild(0).GetComponent<Creature>();

            if (aiCreature.CurrentAttacks >= 1 && aiCreature.CanAttack)
            {
                if(aiCreature.CurrentAttacks == 2)
                {
                    playerCreature.TakeDamage(aiCreature.Strength);
                    aiCreature.TakeDamage(playerCreature.Strength);

                    Debug.Log("First target hit");

                    aiCreature.CurrentAttacks--;

                    if(playerCreature.Health < 1)
                    {
                        Debug.Log("Target killed on first attack");

                        attackTarget = null;
                    }
                }

                if (attackTarget == null)
                {
                    Debug.Log("New target");

                    foreach (Transform target in GameObject.Find("Player Playfield").transform)
                    {
                        if (target.gameObject.transform.GetChild(0).GetComponent<Creature>().HasTaunt)
                        {
                            attackTarget = target.gameObject;
                        }
                        else
                        {
                            attackTarget = TargetList[Random.Range(0, TargetList.Count)];
                        }
                        playerCreature = attackTarget.transform.GetChild(0).GetComponent<Creature>();
                    }

                    if(attackTarget == null)
                    {
                        Debug.Log("no targets");
                        break;
                    }
                }

                if (aiCreature.Health > 0 && playerCreature.Health > 0)
                {
                    playerCreature.TakeDamage(aiCreature.Strength);
                    aiCreature.TakeDamage(playerCreature.Strength);

                    Debug.Log("Second Attack");

                    aiCreature.CurrentAttacks--;
                }
            }
        }
    }
}
