using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text playerTextHP;
    public Text playerTextMana;
    public int playerHP;
    public int playerMana;
    public int playerMaxHP = 30;
    public int playerMaxMana = 1;
    GameManager gameManager;
    CardClass card;

    void Start ()
    {
        playerHP = playerMaxHP;

        playerTextHP.text = playerHP.ToString();

        playerTextMana.text = playerMana.ToString();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        card = this.transform.GetComponent<CardClass>();
    }
	
	void UpdateHP()
    {
        playerTextHP.text = playerHP.ToString();
    }

    void UpdateMana()
    {
        playerTextMana.text = playerMana.ToString();
    }

    public void PlayerAbility()
    {
        if (Hero.priest && gameManager.PlayerTurn == 0 && card.OwnerId == 0)
        {
            PriestAbility();
        }

        if (Hero.warlock && gameManager.PlayerTurn == 0 && card.OwnerId == 0 && playerMana >= 2)
        {
            WarlockAbility();
        }
    }

    void WarlockAbility()
    {
        playerHP -= 2;
        playerMana -= 2;
        UpdateHP();
        UpdateMana();
        //DrawCard();
    }
    
    void PriestAbility()
    {
        playerMana -= 2;
        UpdateMana();
        //point to object and heal object with 2
        GetComponent<BattleTargeting>().OnMouseDrag();
        GetComponent<BattleTargeting>().OnMouseUp();
    }
}
