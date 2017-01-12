using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : MonoBehaviour
{
	protected GameManager gameManager;

	[SerializeField]
	protected GameObject playerHand;
	[SerializeField]
	protected GameObject aiHand;

	protected GameObject[] playerHands = new GameObject[2];

	[SerializeField]
	protected GameObject spellCard, creatureCard;

	protected ParseFromJSON json;
	protected CARDS c;

	protected int[] playerOverdraw = new int[] { 0, 0 };

	// Use this for initialization
	void Start()
	{
		gameManager = GetComponent<GameManager>();
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();
		playerHands[0] = playerHand;
		playerHands[1] = aiHand;
	}

	public void CardDraw(int playerIndex)
	{
		if (gameManager.Decks[playerIndex].Count > 0)
		{
			int whatCard = Random.Range(0, gameManager.Decks[playerIndex].Count);
			print("drawCArd to json" + whatCard);
			c = json.loadFile(gameManager.Decks[playerIndex][whatCard]+1);
			GameObject g;

			//if (c.card_type.ToLower() == "spell")
			//{
			//	g = (Instantiate(spellCard, playerHands[playerIndex].transform, false) as GameObject);
			//	g.transform.GetChild(0).GetComponent<SpellCardGenerator>().GenerateCard(c);
			//}
			//else
			{
				g = (Instantiate(creatureCard, playerHands[playerIndex].transform, false) as GameObject);
				g.transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(c);
			}

			if (playerIndex == 1)
			{
				g.BroadcastMessage("DisableTexts");
                g.transform.GetChild(0).GetComponent<CardClass>().OwnerId = 1;
                g.transform.GetComponent<Draggable>().enabled = false;
            }

			gameManager.Decks[playerIndex].RemoveAt(whatCard);
		}

		else
		{
			OverDraw(playerIndex);
		}
	}

	void OverDraw(int playerIndex)
	{
		playerOverdraw[playerIndex]++;
		gameManager.HeroDamage(playerIndex, playerOverdraw[playerIndex]);
	}


}
