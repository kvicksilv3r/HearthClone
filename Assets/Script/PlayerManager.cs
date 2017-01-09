using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text playerTextHP;
    int playerHP;
    public int playerMaxHP = 30;
    GameManager gameManager;

	void Start ()
    {
        playerHP = playerMaxHP;
        playerTextHP.text = playerHP.ToString();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
	
	void UpdateHP ()
    {
        playerTextHP.text = playerHP.ToString();
    }

    public void PlayerAbility()
    {
        if (Hero.priest && gameManager.PlayerTurn == 0)
        {
            PriestAbility();
        }

        if (Hero.warlock && gameManager.PlayerTurn == 0)
        {
            WarlockAbility();
        }
    }

    public void WarlockAbility()
    {
        playerHP -= 2;
		gameManager.DrawCard(gameManager.PlayerTurn);
        UpdateHP();
        //DrawCard();
    }
    
    void PriestAbility()
    {
        //point to object and heal object with 2
        GetComponent<BattleTargeting>().OnMouseDrag();
        GetComponent<BattleTargeting>().OnMouseUp();
    }
}
