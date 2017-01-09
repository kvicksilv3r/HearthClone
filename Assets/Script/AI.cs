using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{
    GameManager gameManager;
    public Transform enemyPlayField;
    //public List<Transform> HandList;
    int maxMana = 10;
    int aiCurrentMana = 10;
    int aiManaSpent;
    int manaCost;
    public bool aiPlayedCard;

    public void AITurn()
    {
        foreach (Transform child in transform)
        {
            //HandList.Add(child.transform);

            if (child.tag == "Creature")
            {
                Debug.Log("creature found");

                child.GetChild(0).GetComponent<Creature>().OwnerId = 1;

                CheckCurrentMana();

                CheckCardManaCost();

                if (aiCurrentMana >= manaCost)
                {
                    child.SetParent(enemyPlayField);
					GameObject.Find("GameManager").GetComponent<GameManager>().IsSleeping = true;
                    child.rotation = new Quaternion(0, 0, 0, 180);
                    aiManaSpent += manaCost;
                   // HandList.Remove(child.transform);
                    child.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    child.GetComponent<Draggable>().playedCard = true;
                    child.GetComponent<Draggable>().PlayCard();
                    child.GetChild(0).GetComponent<CardGenerator>().PlayedCard();
                }
            }
        }

        GameObject.Find("Enemy Playfield").GetComponent<AIBattle>().AIBattlePhase();
    }

    void CheckCurrentMana()
    {
        aiCurrentMana -= aiManaSpent;
    }

    void CheckCardManaCost()
    {
        manaCost = GetComponentInChildren<CardClass>().CardCost;
    }
		
    void PlaySpell()
    {
        aiManaSpent += manaCost;
    }
}
