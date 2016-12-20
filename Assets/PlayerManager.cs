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
	
	void Update ()
    {
		
	}

    public void PlayerAbility()
    {
        //if(priest)
        //{
        //  PriestAbility();
        //}

        //if(Warlock)
        //{
        //  WarlockAbility();
        //}
    }

    void WarlockAbility()
    {
        playerHP -= 2;
        //DrawCard();
    }
    
    void PriestAbility()
    {
        //point to object and heal object with 2
    }
}
