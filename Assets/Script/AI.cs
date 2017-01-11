using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class AI : MonoBehaviour
{
    GameManager gameManager;
    Text manaText;
    public Transform enemyPlayField;
    //public List<Transform> HandList;
    int aiManaSpent;
    int aiCurrentMana;
    int manaCost;
    public bool aiPlayedCard;

    float waitTime = 2;

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
     
    public void AITurn()
    {
        StartCoroutine("AIPlay");
    }

    IEnumerator AIPlay()
    {
        aiManaSpent = 0;

        aiCurrentMana = gameManager.Players()[1].currentMana;

        foreach (Transform child in transform)
        {
            //HandList.Add(child.transform);

            if (child.tag == "Creature")
            {
                Debug.Log("creature found");

                //child.GetChild(0).GetComponent<Creature>().OwnerId = 1;

                CheckCurrentMana();

                manaCost = child.GetChild(0).GetComponent<Creature>().CardCost;

                if (aiCurrentMana >= manaCost && GameObject.Find("Enemy Playfield").transform.childCount < 7)
                {
                    child.SetParent(enemyPlayField);
                    gameManager.IsSleeping = true;
                    child.rotation = new Quaternion(0, 0, 0, 180);
                    child.position = new Vector3(child.position.x, child.position.y, 7);
                    gameManager.ExpendMana(manaCost);
                    // HandList.Remove(child.transform);
                    child.GetChild(0).GetChild(1).gameObject.SetActive(false);
                    child.GetComponent<Draggable>().playedCard = true;
                    child.GetComponent<Draggable>().PlayCard();
                    child.GetChild(0).GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
        GameObject.Find("Enemy Playfield").GetComponent<AIBattle>().AIBattlePhase();

        yield return null;
    }

    void CheckCurrentMana()
    {
		aiCurrentMana = gameManager.Players()[1].currentMana;
        
    }

    void PlaySpell()
    {
        aiManaSpent += manaCost;
    }
}
