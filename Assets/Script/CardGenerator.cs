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
	GameObject deathRattle, roundStartAbility, timeChangeAbility, attackAbility;


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

		creature.CurrentAttacks = 1;

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
				switch (i)
				{
					case 4:
						NightStalker();
						break;

					case 5:
						GreenFlame();
						break;

					case 7:
						TravelingMerchant();
						break;

					case 8:
						EvilMeda();
						break;

					case 10:
						BloodLord();
						break;

					case 11:
						DevotedWorshipper();
						break;

					case 12:
						DarkAcolyte();
						break;

					case 13:
						OldSeerAbility();
						break;

					case 14:
						FableCreature();
						break;

					case 15:
						YungMartyr();
						break;

					case 16:
						Berzerker();
						break;

					case 17:
						WolfWarrior();
						break;

					case 18:
						Bard();
						break;

					case 19:
						FatAngel();
						break;

					case 20:
						BenevolentSpirit();
						break;

					case 21:
						MaidenOfFavor();
						break;

					case 22:
						CrusaderOfTheLight();

						break;

					case 23:
						BringerOfJustice();
						break;

					case 25:
						Wyvern();
						break;

					case 27:
						Exorcist();
						break;

					case 28:
						Ancestor();
						break;

					case 29:
						ForgottenGod();
						break;

					case 30:
						MoonlightAssassin();
						break;

					case 31:
						WitchHunter();
						break;

					case 32:
						Scout();
						break;
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

		foreach (Creature cr in gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>())
		{
			if (cr.CardId == 12 && cr != creature)
			{
				OldSeerBuff();
			}
		}

		healthTextObj.transform.position = hpPos.transform.position - new Vector3(0, 0, 0.2f);
		damageTextObj.transform.position = dmgPos.transform.position - new Vector3(0, 0, 0.2f);
		UpdateText();
	}

	void NightStalker()
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

		GameObject timeChange = Instantiate(timeChangeAbility, transform, false);
	}

	void GreenFlame()
	{
		if (gameManager.TimeIndex == 1)
		{
			creature.Strength -= 1;
			creature.Health -= 1;
		}
		else if (gameManager.TimeIndex == 2)
		{
			creature.Strength += 1;
			creature.Health += 1;
		}
		GameObject timeChange = Instantiate(timeChangeAbility, transform, false);
	}

	void DarkAcolyte()
	{
		GameObject roundEnd = Instantiate(endRoundStuff, transform, false);
	}

	void DevotedWorshipper()
	{
		gameManager.HeroDamage(creature.OwnerId, 2);
	}

	void EvilMeda()
	{
		Instantiate(roundStartAbility, transform, false);
	}

	void BloodLord()
	{
		if (gameManager.TimeIndex == 1)
		{
			creature.Strength -= 2;
		}

		creature.AttackAbility = true;

		Instantiate(timeChangeAbility, transform, false);
		Instantiate(attackAbility, transform, false);
	}

	void OldSeerAbility()
	{
		creature.Deathrattle = true;

		Instantiate(deathRattle, transform, false);

		if (gameManager.GetNumberOnBoard(creature.OwnerId) > 0)
		{
			foreach (Creature cr in gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>())
			{
				cr.Strength += 2;
				cr.Health += 1;
			}
		}
	}

	void OldSeerBuff()
	{
		creature.Strength += 2;
		creature.Health += 1;
	}

	void YungMartyr()
	{
		Instantiate(deathRattle, transform, false);
	}

	void Bard()
	{
		if (gameManager.TimeIndex == 2)
		{
			creature.Strength -= 1;
			creature.Health -= 1;
		}

		GameObject timeChange = Instantiate(timeChangeAbility, transform, false);

	}

	void Berzerker()
	{
		if (gameManager.TimeIndex == 2)
		{
			creature.MaxAttacks = 2;
			creature.CurrentAttacks = 2;
		}
		Instantiate(timeChangeAbility, transform, false);
	}

	void TravelingMerchant()
	{
		if (gameManager.Boards[creature.OwnerId].transform.childCount > 0)
		{
			int creatureIndex = Random.Range(0, gameManager.Boards[creature.OwnerId].transform.childCount);
            gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>()[creatureIndex].GetTaunt();
			gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>()[creatureIndex].Health += 2;
		}
	}

	void Ancestor()
	{
		if (gameManager.GetNumberOnBoard(creature.OwnerId) <= 0)
		{
			print(gameManager.GetNumberOnBoard(creature.OwnerId));
			creature.Strength += 1;
			creature.Health += 1;
		}
	}

	void Exorcist()
	{
		List<Creature> demons = new List<Creature>();

		if (gameManager.TimeIndex == 1)
		{
			foreach (Creature cr in gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.GetComponentsInChildren<Creature>())
			{
				if (cr.Race.ToLower() == "demon")
				{
					demons.Add(cr);
				}
			}

			if (demons.Count > 0)
			{
				demons[Random.Range(0, demons.Count)].Death(true);
			}
		}
	}

	void MoonlightAssassin()
	{
		if (gameManager.TimeIndex == 2)
		{
			creature.CanAttack = true;
			creature.CurrentAttacks = creature.MaxAttacks;
		}

		Instantiate(attackAbility, transform, false);

		creature.AttackAbility = true;
	}

	void Wyvern()
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

	void WitchHunter()
	{
		if(gameManager.TimeIndex == 2)
		{
			creature.Strength += 1;
		}
		Instantiate(timeChangeAbility, transform, false);
	}

	void BenevolentSpirit()
	{
		if (gameManager.TimeIndex == 1)
		{
			creature.Strength += 1;
		}
		else if (gameManager.TimeIndex == 2)
		{
			creature.Strength -= 1;
		}
		Instantiate(timeChangeAbility, transform, false);
	}

	void MaidenOfFavor()
	{
		Instantiate(roundStartAbility, transform, false);
	}

	void Scout()
	{
		if (gameManager.Boards[Mathf.Abs(creature.OwnerId + 1 - 2)].transform.childCount > 0)
		{
			foreach (Creature cr in gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>())
			{
				cr.CanAttack = true;
				gameManager.StopSleeping();
			}
        }
    }

	void WolfWarrior()
	{
		if (gameManager.GetNumberOnBoard(creature.OwnerId) > 0)
		{
			foreach (Creature cr in gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>())
			{
				creature.Health += 1;

				if(gameManager.TimeIndex == 2)
				{
					creature.Strength += 2;
				}
				else
				{
					creature.Strength += 1;
				}
			}
		}
	}

	void FatAngel()
	{
		int random = Random.Range(0, 100);
		print(random);
		if (random > 50)
		{
			gameManager.DrawCard(creature.OwnerId);
			gameManager.DrawCard(creature.OwnerId);
		}
	}

	void ForgottenGod()
	{
		if (gameManager.Players()[creature.OwnerId].health < gameManager.Players()[Mathf.Abs(creature.OwnerId + 1 - 2)].health)
		{
			print("hp lower");
			if (gameManager.GetNumberOnBoard(creature.OwnerId) > 0)
			{
				print("creatures on your side");
				foreach (Creature cr in gameManager.Boards[creature.OwnerId].transform.GetComponentsInChildren<Creature>())
				{
					print("should be buffd");
					cr.Strength += 1;
					cr.Health += 2;
				}
			}
		}
	}

	void CrusaderOfTheLight()
	{
		if (gameManager.TimeIndex == 1)
		{
			creature.Strength += 2;
		}

		Instantiate(timeChangeAbility, transform, false);
	}

	void BringerOfJustice()
	{
		gameManager.HeroDamage(Mathf.Abs(creature.OwnerId + 1 - 2), 3);
		gameManager.HeroDamage(creature.OwnerId, -5);
	}

	void FableCreature()
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
