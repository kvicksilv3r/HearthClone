using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
	public string cardPictureName;
	protected Creature creature;
	protected GameManager gameManager;
	string pictureAssetName;
	[SerializeField]
	GameObject portrait;
	[SerializeField]
	GameObject healthTextObj;
	[SerializeField]
	GameObject damageTextObj;
	[SerializeField]
	GameObject manaTextObj;
	[SerializeField]
	GameObject cardTextObj;
	[SerializeField]
	GameObject cardNameTextObj;
	[SerializeField]
	GameObject cardRaceTextObj;
	[SerializeField]
	GameObject cardRaceObj;
	[SerializeField]
	GameObject portraitObj;
	[SerializeField]
	GameObject dragonObj;
	[SerializeField]
	GameObject gemObj;
	[SerializeField]
	GameObject gemHolderObj;
	[SerializeField]
	GameObject cardFaceObj;
	[SerializeField]
	GameObject dmgPos;
	[SerializeField]
	GameObject hpPos;
	[SerializeField]
	GameObject portraitBorder;
	[SerializeField]
	GameObject tauntObj;
	[SerializeField]
	GameObject sleepingParticle;
	[SerializeField]
	GameObject endRoundStuff;
	[SerializeField]
	GameObject deathRattle, startRoundAbility;
	ParseFromJSON json;
	CARDS card;
	
	public void GenerateCard(int cardId)
	{
		card = GameObject.Find("GameManager").GetComponent<ParseFromJSON>().loadFile(cardId);
		GenerateCard();
	}

	public void GenerateCard(CARDS cardd)
	{
		card = cardd;
		GenerateCard();
	}


	public void GenerateCard()
	{
		
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		creature = GetComponent<Creature>();
		GetComponent<CardClass>().Card = card;

		pictureAssetName = "Cards/Textures/" + card.picture_name;
		print(pictureAssetName);
		portrait.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(pictureAssetName) as Texture;

		UpdateText();
		manaTextObj.GetComponent<Text>().text = card.mana.ToString();
		cardTextObj.GetComponent<Text>().text = card.description;
		cardNameTextObj.GetComponent<Text>().text = card.card_name;

		creature.Health = card.health;
		creature.Strength = card.damage;
		creature.CardCost = card.mana;
		creature.Abilities = card.abilities_cr;


		if (card.race != "none")
		{
			cardRaceTextObj.GetComponent<Text>().text = card.race;
			cardRaceObj.SetActive(true);
			creature.Race = card.race;
		}

		if (card.rarity.ToLower() == "legendary")
		{
			dragonObj.SetActive(true);
		}

		creature.Strength = card.damage;
		creature.MaxAttacks = 1;
		creature.CurrentAttacks = 0;
		creature.CardId = card.card_id;
		creature.Deathrattle = card.deathrattle;

		if (card.abilities_cr.GetLength(0) > 0)
		{
			foreach (int i in card.abilities_cr)
			{
				if (i == 1)
				{
					creature.HasTaunt = true;
				}
				else if (i == 2)
				{
					creature.CanAttack = true;
				}
				else if (i == 3)
				{
					creature.MaxAttacks = 2;
				}
			}
		}

		if (card.day_or_night != "none")
		{
			creature.TimeEffect = true;
		}
		else
		{
			creature.TimeEffect = false;
		}

		if (creature.CanAttack)
		{
			creature.CurrentAttacks = creature.MaxAttacks;
		}

		gemHolderObj.SetActive(true);
		gemObj.SetActive(true);

		gemObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Gems/gem_" + card.rarity) as Texture2D;

		cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Cardfronts/card_minion_" + card.class_name) as Texture;
		transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
	}

	public void PlayedCard()
	{
		hpPos.SetActive(true);
		dmgPos.SetActive(true);

		if (card.deathrattle)
		{
			Instantiate(deathRattle, transform, false);
		}

		if (card.abilities_cr.GetLength(0) > 0)
		{
			foreach (int i in card.abilities_cr)
			{
				if (i == 4)
				{
					NightStalkerAbility();
                }

				else if(i == 5)
				{
					if (gameManager.TimeIndex == 1)
					{
						creature.Strength -= 3;
						creature.Health -= 3;
					}
				}
				else if (i == 11)
				{
					gameManager.HeroDamage(creature.OwnerId, 2);
				}
				else if (i == 12)
				{
					GameObject roundEnd = Instantiate(endRoundStuff, transform, false);
					roundEnd.GetComponent<RoundEndStuff>().effect = i;
				}
				else if (i == 14)
				{
					FableCreatureAbility();
				}

				else if (i == 16)
				{
					if(gameManager.TimeIndex == 2)
					{
						creature.MaxAttacks = 2;
					}
				}

				else if (i == 19)
				{
					MissionaryAbility();
				}

				else if (i == 22)
				{
					if (gameManager.TimeIndex == 1)
					{
						creature.Strength += 2;
					}
				}

				else if (i == 23)
				{
					gameManager.HeroDamage(Mathf.Abs(creature.OwnerId + 1 - 2), 3);
					gameManager.HeroDamage(creature.OwnerId, -5);
				}

				else if (i == 25)
				{
					WyvernAbility();
				}

				else if (i == 27)
				{
					if(gameManager.TimeIndex == 1)
					{
						ExorcistAbility();
					}
				}
			}
		}

		if (!GetComponent<Creature>().CanAttack)
		{
			sleepingParticle.SetActive(true);
		}

		portraitBorder.SetActive(true);

		if (GetComponent<Creature>().HasTaunt)
		{
			tauntObj.SetActive(true);
		}


		transform.FindChild("Card Stuff").gameObject.SetActive(false);

		healthTextObj.transform.position = hpPos.transform.position - new Vector3(0, 0, 0.2f);
		damageTextObj.transform.position = dmgPos.transform.position - new Vector3(0, 0, 0.2f);
		UpdateText();
	}

	void NightStalkerAbility()
	{
		if (gameManager.TimeIndex == 2)
		{
			creature.Strength += 2;
			creature.Health -= 1;
		}
		else if (gameManager.TimeIndex == 1)
		{
			creature.Strength -= 1;
			creature.Health += 2;
		}
	}

	void ExorcistAbility()
	{
		List<Creature> demons = new List<Creature>();

		foreach (Creature cr in gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.GetComponentsInChildren<Creature>())
		{
			if(cr.Race.ToLower() == "demon")
			{
				demons.Add(cr);
			}
		}

		if (demons.Count > 0)
		{
			demons[Random.Range(0, demons.Count)].Death(true);
		}
    }

	void WyvernAbility()
	{
		if (gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.childCount <= 0)
		{
			gameManager.HeroDamage(Mathf.Abs(creature.OwnerId + 1 - 2), 6);
		}

		else
		{
			List<Creature> toDie = new List<Creature>();

			foreach (Creature cr in gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.GetComponentsInChildren<Creature>())
			{
				if (!cr.IsDead)
				{
					toDie.Add(cr);
				}
			}

			for (int o = 0; o < 6; o++)
			{
				if (toDie.Count > 0 && Random.Range(0, 2) == 0)
				{
					int whatToDie = Random.Range(0, toDie.Count);
					toDie[whatToDie].TakeDamage(1);
					if (toDie[whatToDie].IsDead)
					{
						toDie.RemoveAt(whatToDie);
					}
				}
				else
				{
					gameManager.HeroDamage(Mathf.Abs(creature.OwnerId + 1 - 2), 1);
				}

			}
		}
	}

	void MissionaryAbility()
	{
		int random = Random.Range(0, 100);
		print(random);
		if (random > 50)
		{
			gameManager.DrawCard(creature.OwnerId);
			gameManager.DrawCard(creature.OwnerId);
		}
	}

	void FableCreatureAbility()
	{

		List<Creature> toDie = new List<Creature>();
		if (gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.childCount > 0)
		{
			print("creatures found on the other board");
			foreach (Creature cr in gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.GetComponentsInChildren<Creature>())
			{
				if (cr.Health <= creature.Health)
				{
					toDie.Add(cr);
				}
			}

			if (toDie.Count > 0)
			{
				int whatToDie = Random.Range(0, toDie.Count);
				print("should DIE");
				toDie[whatToDie].StartCoroutine("Death", true);
			}
		}
	}

	void UpdateText()
	{
		healthTextObj.GetComponent<Text>().text = creature.Health.ToString();
		damageTextObj.GetComponent<Text>().text = creature.Strength.ToString();
	}

}
