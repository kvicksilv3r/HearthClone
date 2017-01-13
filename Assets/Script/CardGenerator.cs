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
	GameObject deathRattle;
	ParseFromJSON json;
	CARDS c;

	void Start()
	{
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();

	}

	public void GenerateCard(int card)
	{
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();
		c = json.loadFile(card);
		GenerateCard(c);
	}

	public void GenerateCard(CARDS card)
	{
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();
		//c = json.loadFile(card.card_id);

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		creature = GetComponent<Creature>();
		c = card;
		GetComponent<CardClass>().Card = card;
		
		pictureAssetName = "Cards/Textures/" + c.picture_name;
		print(pictureAssetName);
		portrait.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(pictureAssetName) as Texture;

		UpdateText();
		manaTextObj.GetComponent<Text>().text = c.mana.ToString();
		cardTextObj.GetComponent<Text>().text = c.description;
		cardNameTextObj.GetComponent<Text>().text = c.card_name;

		creature.Health = c.health;
		creature.Strength = c.damage;
		creature.CardCost = c.mana;


		if (c.race != "none")
		{
			cardRaceTextObj.GetComponent<Text>().text = c.race;
			cardRaceObj.SetActive(true);
		}

		if (c.rarity.ToLower() == "legendary")
		{
			dragonObj.SetActive(true);
		}

		creature.Strength = c.damage;
		creature.MaxAttacks = 1;
		creature.CurrentAttacks = 0;
		creature.CardId = c.card_id;
		creature.Deathrattle = c.deathrattle;

		if (c.abilities_cr.GetLength(0) > 0)
		{
			foreach (int i in c.abilities_cr)
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
				else if (i == 5)
				{
					if (gameManager.TimeIndex == 1)
					{
						creature.Strength -= 3;
						creature.Health -= 3;
					}
				}
				else if (i == 6)
				{
					//day -3-3
				}

				else if (i == 7)
				{
					//give +0+2 +taunt 
					// give as spell?
				}

				else if (i == 8)
				{
					//attach object, deal 1 dmg on round start
				}
				if (i == 9)
				{
					//no heals
				}
				else if (i == 10)
				{
					//night lifelink; day -2-0
				}

			}
		}

		if (c.day_or_night != "none")
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

		gemObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Gems/gem_" + c.rarity) as Texture2D;

		cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Cardfronts/card_minion_" + c.class_name) as Texture;
		transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
	}

	public void PlayedCard()
	{
		hpPos.SetActive(true);
		dmgPos.SetActive(true);

		if (c.deathrattle)
		{
			Instantiate(deathRattle, transform, false);
		}

		if (c.abilities_cr.GetLength(0) > 0)
		{
			foreach (int i in c.abilities_cr)
			{
				if (i == 4)
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
				if (i == 11)
				{
					gameManager.HeroDamage(creature.OwnerId, 2);
				}
				if (i == 12)
				{
					GameObject roundEnd = Instantiate(endRoundStuff, transform, false);
					roundEnd.GetComponent<RoundEndStuff>().effect = i;
				}
				if (i == 14)
				{
					FableCreatureAbility();
				}
				if (i == 19)
				{
					MissionaryAbility();
				}

				if (i == 22)
				{
					if (gameManager.TimeIndex == 1)
					{
						creature.Strength += 2;
					}
				}

				if (i == 23)
				{
					gameManager.HeroDamage(Mathf.Abs(creature.OwnerId + 1 - 2), 5);
				}

				if (i == 25)
				{
					WyvernAbility();
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
				toDie[whatToDie].TakeDamage(toDie[whatToDie].Health);
			}
		}
	}

	void UpdateText()
	{
		healthTextObj.GetComponent<Text>().text = creature.Health.ToString();
		damageTextObj.GetComponent<Text>().text = creature.Strength.ToString();
	}

}
