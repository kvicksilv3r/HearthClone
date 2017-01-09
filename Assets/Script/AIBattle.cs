using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
    //GameObject[] targets;

    List<GameObject> TargetList;
    GameObject attackTarget;
    Creature creature;

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
        }

        foreach (Transform child in transform)
        {
            //targets = new GameObject[GameObject.Find("Player Playfield").transform.childCount];
            if (child.GetChild(0).GetComponent<Creature>().CurrentAttacks >= 1 && child.GetChild(0).GetComponent<Creature>().CanAttack)
            {
                if(child.GetChild(0).GetComponent<Creature>().CurrentAttacks == 2)
                {
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().Health -= child.GetChild(0).GetComponent<Creature>().Strength;
                    child.GetChild(0).GetComponent<Creature>().Health -= attackTarget.transform.GetChild(0).GetComponent<Creature>().Strength;
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().UpdateHP();
                    child.GetChild(0).GetComponent<Creature>().UpdateHP();
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().CheckHealth(attackTarget.transform.GetChild(0).GetComponent<Creature>().Health);
                    child.GetChild(0).GetComponent<Creature>().CheckHealth(child.GetChild(0).GetComponent<Creature>().Health);
                    Debug.Log("First target hit");

                    child.GetChild(0).GetComponent<Creature>().CurrentAttacks--;

                    if (attackTarget.transform.GetChild(0).GetComponent<Creature>().Health < 1)
                    {
                        Destroy(attackTarget.gameObject);
                        attackTarget = null;
                    }
                    if (child.GetChild(0).GetComponent<Creature>().Health < 1)
                    {
                        Destroy(child.gameObject);
                    }
                }

                if (attackTarget == null)
                {
                    Debug.Log("New target");
                    attackTarget = TargetList[Random.Range(0, TargetList.Count)];
                    if(attackTarget == null)
                    {
                        Debug.Log("no targets");
                        break;
                    }
                }

                attackTarget.transform.GetChild(0).GetComponent<Creature>().Health -= child.GetChild(0).GetComponent<Creature>().Strength;
                child.GetChild(0).GetComponent<Creature>().Health -= attackTarget.transform.GetChild(0).GetComponent<Creature>().Strength;
                attackTarget.transform.GetChild(0).GetComponent<Creature>().UpdateHP();
                child.GetChild(0).GetComponent<Creature>().UpdateHP();
                attackTarget.transform.GetChild(0).GetComponent<Creature>().CheckHealth(attackTarget.transform.GetChild(0).GetComponent<Creature>().Health);
                child.GetChild(0).GetComponent<Creature>().CheckHealth(child.GetChild(0).GetComponent<Creature>().Health);

                Debug.Log("Second Attack");

                child.GetChild(0).GetComponent<Creature>().CurrentAttacks--;

                if (attackTarget.transform.GetChild(0).GetComponent<Creature>().Health < 1)
                {
                    Destroy(attackTarget.gameObject);
                }
                if (child.GetChild(0).GetComponent<Creature>().Health < 1)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
