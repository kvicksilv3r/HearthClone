using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBattle : MonoBehaviour
{
    //GameObject[] targets;

    List<GameObject> TargetList;
    GameObject target;
    Creature creature;

    public void AIBattlePhase()
    {
        TargetList = new List<GameObject>(GameObject.Find("Player Playfield").transform.childCount);

        foreach (Transform target in GameObject.Find("Player Playfield").transform)
        {
            TargetList.Add(target.gameObject);
        }

        foreach (Transform child in transform)
        {
            if (child.GetChild(0).GetComponent<Creature>().CanAttack)
            {
                //targets = new GameObject[GameObject.Find("Player Playfield").transform.childCount];
                target = TargetList[Random.Range(0, TargetList.Count)];
                
                target.transform.GetChild(0).GetComponent<Creature>().Health -= child.GetChild(0).GetComponent<Creature>().Strength;
                child.GetChild(0).GetComponent<Creature>().Health -= target.transform.GetChild(0).GetComponent<Creature>().Strength;

            }
        }
    }
}
