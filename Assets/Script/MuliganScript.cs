using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class MuliganScript : MonoBehaviour
{

	protected GameManager gameManager;

	protected List<GameObject> mulCards = new List<GameObject>();
	protected GameObject[] muliganSelectors = new GameObject[4];

	[SerializeField]
	protected GameObject muliganSelctor;
	[SerializeField]
	protected GameObject theCoin;

	[SerializeField]
	protected GameObject playerHand;
	[SerializeField]
	protected GameObject aiHand;

	[SerializeField]
	protected GameObject spellCard, creatureCard;

	protected List<int> cardsToHand = new List<int>();

	protected List<int>[] decks = new List<int>[2];
	protected ParseFromJSON json;
	protected int[] aiMulCosts = new int[4];

	protected int numCards;

	protected List<CARDS> aiCards = new List<CARDS>();
	protected List<CARDS> aiMul = new List<CARDS>();

	CARDS c;

	[SerializeField]
	List<int> discardedCards = new List<int>();

	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void BeginMuligan(int numberOfCards)
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		decks = gameManager.Decks;
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();

		numCards = numberOfCards;

		//if(numberOfCards == 4)
		//{
		//	Instantiate(theCoin, playerHand.transform, false);
		//}
		//else
		//{
		//	Instantiate(theCoin, aiHand.transform, false);
		//}

		for (int i = 0; i < numberOfCards; i++)
		{
			int whatCard = Random.Range(1, decks[0].Count-1);
			print(whatCard);
			
			c = json.loadFile(decks[0][whatCard+1]);


			if (c.card_type.ToLower() == "spell")
			{
				mulCards.Add(Instantiate(spellCard, transform, false) as GameObject);
				mulCards[i].transform.GetChild(0).GetComponent<SpellCardGenerator>().GenerateCard(c);
			}
			else
			{
				mulCards.Add(Instantiate(creatureCard, transform, false) as GameObject);
				mulCards[i].transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(c);
			}

			muliganSelectors[i] = Instantiate(muliganSelctor, mulCards[i].transform, false);
			muliganSelectors[i].transform.localPosition = new Vector3(0, 0, 0);
			muliganSelectors[i].GetComponent<MuliganSelector>().id = i;
			mulCards[i].GetComponent<Draggable>().enabled = false;
			decks[0].RemoveAt(whatCard - 1);
		}

		AiMul();
	}

	void AiMul()
	{
		for (int i = 0; i < 3 + (4 - numCards); i++)
		{
			int whatAiCard = Random.Range(0, decks[1].Count);
			c = json.loadFile(decks[1][whatAiCard]);
			aiCards.Add(c);
			print(c.card_name);
			decks[1].RemoveAt(whatAiCard);
		}

		foreach (CARDS card in aiCards)
		{
			if (card.mana > 4)
			{
				aiMul.Add(card);
			}
		}

		if (aiMul.Count > 0)
		{
			foreach (CARDS mul in aiMul)
			{
				decks[1].Add(mul.card_id);
				aiCards.Remove(mul);
			}

			foreach(CARDS mul in aiMul)
			{
				int whatAiCard = Random.Range(0, decks[1].Count);
				aiCards.Add(json.loadFile(decks[1][whatAiCard+1]));
				decks[1].RemoveAt(whatAiCard);
			}
		}
	}

	public void EndMuligan()
	{
		if (discardedCards.Count > 0)
		{
			foreach (int discard in discardedCards)
			{
				decks[0].Add(mulCards[discard].transform.GetChild(0).GetComponent<CardClass>().Card.card_id);
			}

			foreach (int discard in discardedCards)
			{
				int whatCard = Random.Range(0, decks[0].Count);
				mulCards[discard].transform.GetChild(0).GetComponent<CardClass>().Card.card_id = json.loadFile(decks[0][whatCard]).card_id;
				decks[0].RemoveAt(whatCard);
			}
		}

		foreach (GameObject g in mulCards)
		{
			cardsToHand.Add(g.transform.transform.GetChild(0).GetComponent<CardClass>().Card.card_id);
		}

		foreach (int card in cardsToHand)
		{

			c = json.loadFile(card);
			GameObject g;

			if (c.card_type.ToLower() == "spell")
			{
				g = Instantiate(spellCard, playerHand.transform, false) as GameObject;
				g.GetComponent<SpellCardGenerator>().GenerateCard(c);
			}
			else
			{
				g = Instantiate(creatureCard, playerHand.transform, false) as GameObject;
				g.transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(c);
			}
		}

		foreach(CARDS aiCard in aiCards)
		{
			GameObject g;

			if (c.card_type.ToLower() == "spell")
			{
				g = Instantiate(spellCard, aiHand.transform, false) as GameObject;
				g.GetComponent<SpellCardGenerator>().GenerateCard(aiCard);
			}
			else
			{
				g = Instantiate(creatureCard, aiHand.transform, false) as GameObject;
				g.transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(aiCard);
			}

			g.BroadcastMessage("DisableTexts");
            g.transform.GetChild(0).GetComponent<CardClass>().OwnerId = 1;
            g.transform.GetComponent<Draggable>().enabled = false;
        }

		Destroy(gameObject);
	}

	public void ChoseDiscard(int discardIndex)
	{
		if (discardedCards.Count > 0)
		{
			if (discardedCards.Contains(discardIndex))
			{
				discardedCards.Remove(discardIndex);
			}

			else
			{
				discardedCards.Add(discardIndex);
			}
		}

		else
		{
			discardedCards.Add(discardIndex);
		}
	}
}
