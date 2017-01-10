using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
	ParseFromJSON json;
	CARDS c;

	void Start()
	{
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			//GenerateCard(Random.Range(1, 5));
		}
	}

	public void GenerateCard(CARDS card)
	{

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		creature = GetComponent<Creature>();
		c = card;
		GetComponent<CardClass>().Card = card;

		pictureAssetName = "Assets/Cards/Textures/" + c.picture_name + ".png";
		portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));

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

		gemObj.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/Gems/gem_" + c.rarity + ".png", typeof(Texture2D));

		cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/Cardfronts/card_minion_" + c.class_name + ".png", typeof(Texture2D));
		transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
	}

	public void PlayedCard()
	{
		hpPos.SetActive(true);
		dmgPos.SetActive(true);


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

		healthTextObj.transform.position = hpPos.transform.position - new Vector3(0, 0, 0.2f);
		damageTextObj.transform.position = dmgPos.transform.position - new Vector3(0, 0, 0.2f);
		UpdateText();
	}

	void UpdateText()
	{
		healthTextObj.GetComponent<Text>().text = c.health.ToString();
		damageTextObj.GetComponent<Text>().text = c.damage.ToString();
	}

}
