using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
    //GameObject[] targets;

    List<GameObject> TargetList;
    GameObject attackTarget;
    Creature creature;
    GameObject priority;

    public void AIBattlePhase()
    {
        TargetList = new List<GameObject>(GameObject.Find("Player Playfield").transform.childCount);

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            TargetList.Add(target.gameObject);

            if (target.gameObject.transform.GetChild(0).GetComponent<Creature>().HasTaunt)
            {
                priority = target.gameObject;
                attackTarget = priority;
            }
            else
            {
                attackTarget = TargetList[Random.Range(0, TargetList.Count)];
            }
        }

            foreach (Transform child in transform)
        {
            //if (child.GetChild(0).GetComponent<Creature>().CanAttack)
            {

                //targets = new GameObject[GameObject.Find("Player Playfield").transform.childCount];
                if (child.GetChild(0).GetComponent<Creature>().CanAttack)
                {
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().Health -= child.GetChild(0).GetComponent<Creature>().Strength;
                    child.GetChild(0).GetComponent<Creature>().Health -= attackTarget.transform.GetChild(0).GetComponent<Creature>().Strength;
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().UpdateHP();
                    child.GetChild(0).GetComponent<Creature>().UpdateHP();
                    attackTarget.transform.GetChild(0).GetComponent<Creature>().CheckHealth(attackTarget.transform.GetChild(0).GetComponent<Creature>().Health);
                    child.GetChild(0).GetComponent<Creature>().CheckHealth(child.GetChild(0).GetComponent<Creature>().Health);
                }
            }
        }
    }
}
