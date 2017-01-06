using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class MuliganScript : MonoBehaviour
{

	protected GameManager gameManager;

	protected List<GameObject> mulCards = new List<GameObject>();
	protected GameObject[] muliganSelectors = new GameObject[4];

	[SerializeField]
	protected GameObject muliganSelctor;

	[SerializeField]
	protected GameObject playerHand;

	[SerializeField]
	protected GameObject spellCard, creatureCard;

	protected List<int> cardsToHand = new List<int>();

	protected List<int>[] decks = new List<int>[2];
	protected ParseFromJSON json;
	protected int[] aiMulCosts = new int[4];

	protected int numCards;

	CARDS c;

	[SerializeField]
	List<int> discardedCards = new List<int>();

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		decks = gameManager.Decks;
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void BeginMuligan(int numberOfCards)
	{
		numCards = numberOfCards;

		for (int i = 0; i < numberOfCards; i++)
		{
			int whatCard = Random.Range(1, decks[0].Count + 1);
			c = json.loadFile(whatCard);


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
			muliganSelectors[i].GetComponent<MuliganSelector>().id = i;
			mulCards[i].GetComponent<Draggable>().enabled = false;
			decks[0].RemoveAt(whatCard - 1);
		}
	}

	void AiMul()
	{

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
