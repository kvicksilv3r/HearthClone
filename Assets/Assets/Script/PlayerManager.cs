using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public Text playerTextHP;
    int playerHP;
    public int playerMaxHP = 30;

	void Start ()
    {
        playerHP = playerMaxHP;
        playerTextHP.text = playerHP.ToString();
	}
	
	void UpdateHP ()
    {
        playerTextHP.text = playerHP.ToString();
    }

    public void PlayerAbility()
    {
        if (Hero.priest)
        {
            PriestAbility();
        }

        if (Hero.warlock)
        {
            WarlockAbility();
        }
    }

    void WarlockAbility()
    {
        playerHP -= 2;
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
