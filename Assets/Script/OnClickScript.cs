using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnClickScript : MonoBehaviour
{

	[SerializeField]
	protected string action;
	protected GameManager gameManager;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	void OnMouseDown()
	{
		if (!gameManager.IsPlaying)
		{
			if (action == "mulligan")
				gameManager.EndMuligan();
		}

		if (gameManager.PlayerTurn == 0 && gameManager.IsPlaying)
		{
			switch (action)
			{
				case "turn":
					gameManager.NextRound(false);
					break;

				case "tap":
					if (gameManager.UsedHeroPower(gameManager.PlayerTurn) != true && gameManager.Players()[gameManager.PlayerTurn].currentMana >= 2)
					{
						gameManager.ExpendMana(2);
						gameManager.DrawCard(gameManager.PlayerTurn);
						gameManager.HeroDamage(gameManager.PlayerTurn, 2);
						gameManager.UseHeroPower(gameManager.PlayerTurn);	
					}
					break;

			}
		}
	}


}
