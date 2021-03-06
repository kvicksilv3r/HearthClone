﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float roundTime; //Counts down the current time
	bool youWon = false;
	bool youLost = false;
	[SerializeField]
	GameObject win;
	[SerializeField]
	GameObject lost;
	public bool tauntOnField;
	[SerializeField]
	protected float maxTime; // How long will a round be?
	protected int whosTurn = 0;
	protected bool ropeBurning = false;
	[SerializeField]
	protected GameObject burningRope;
	protected DrawManager drawManager;
	protected ManaManager manaManager;
	protected int timeIndex = 0; //0 = dawn, 1 = day, 2 = night
	protected int roundsBetweenTimeChange = 2;
	protected int currentRoundstoTimeChange; //Current rounds between timechange
	protected List<int>[] decks = new List<int>[2];//Guess we should keep the decks here
	
	protected int[] creatureNumOnBoard = new int[] { 0, 0 }; //Keeps track of numbers of creatures on the board
	protected Player[] players = new Player[2];
	protected bool isSleeping = false;

	protected int[] lastDamage = new int[] { 0, 0 };

	protected bool coinUsed = false;
	protected int coinPlayer;
	[SerializeField]
	protected float muliganTime = 20;
	protected MuliganScript muliganScript;
	protected bool isPlaying = false;

	[SerializeField]
	protected Creature[] lastAttacked = new Creature[2];

	[SerializeField]
	protected GameObject playerHero;
	[SerializeField]
	protected GameObject enemyHero;

	protected GameObject[] heroes = new GameObject[2];

	[SerializeField]
	protected Texture2D cursorTexture;
	[SerializeField]
	protected Texture2D cursorDownTexture;
	protected CursorMode cursorMode = CursorMode.Auto;
	protected Vector2 hotSpot = Vector2.zero;

	protected string[] timeNames = new string[] { "DAWN", "DAY", "NIGHT" };

	[SerializeField]
	protected GameObject damageDisplay;

	protected bool[] heroPowerUsed = new bool[] { false, false };

	protected Text[] manaTexts = new Text[2];
	[SerializeField]
	protected Text[] healthTexts = new Text[2];

	protected GameObject[] boards = new GameObject[2];

	[SerializeField]
	protected GameObject timeTextObject;

	#region LightVars
	protected Light dirLight;
	[SerializeField]
	protected Color[] timeColors;
	[SerializeField]
	protected float lightTransTime;
	#endregion

	void Start()
	{
		//TODO: Ladda in båda decksen till decks här.

		players[0] = new Player();
		players[1] = new Player();
		whosTurn = Random.Range(0, 2);
		roundTime = maxTime;
		currentRoundstoTimeChange = roundsBetweenTimeChange;
		dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
		manaManager = GameObject.Find("ManaManager").GetComponent<ManaManager>();

		manaTexts[0] = GameObject.Find("PlayerManaText").GetComponent<Text>();
		manaTexts[1] = GameObject.Find("AiManaText").GetComponent<Text>();

		healthTexts[1] = GameObject.Find("Enemy Hp").GetComponent<Text>();
		healthTexts[0] = GameObject.Find("Player HP").GetComponent<Text>();

		drawManager = GetComponent<DrawManager>();

		decks[0] = new List<int>();
		decks[1] = new List<int>();

		heroes[0] = GameObject.Find("Player Hero");
		heroes[1] = GameObject.Find("Enemy Hero");

		Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);

		UpdateHealth();

		for (int i = 0; i < 30; i++)
		{
			decks[0].Add(Random.Range(1, 36));
			decks[1].Add(Random.Range(1, 36));
		}

		muliganScript = GameObject.Find("Muligan").GetComponent<MuliganScript>();

		boards[0] = GameObject.Find("Player Playfield");
		boards[1] = GameObject.Find("Enemy Playfield");

		muliganScript.BeginMuligan(3 + whosTurn);
	}
	
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			AddMana();
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			RemoveMana(0);
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			ExpendMana(1);
		}

		if (Input.GetKeyDown(KeyCode.R))
		{
			NextRound(false);
		}

		if (Input.GetMouseButtonDown(0))
		{
			Cursor.SetCursor(cursorDownTexture, hotSpot, cursorMode);
		}
		if (Input.GetMouseButtonUp(0))
		{
			Cursor.SetCursor(cursorTexture, hotSpot, cursorMode);
		}

		if (isPlaying)
		{
			roundTime -= Time.deltaTime;

			if (roundTime <= 20 && !ropeBurning)
			{
				StartRope();
			}

			if (roundTime <= 0)
			{
				NextRound(true);
			}
		}

		else
		{
			muliganTime -= Time.deltaTime;
			if (muliganTime <= 0)
			{
				EndMuligan();
			}
		}
	}

	public void NextRound(bool timeOut)
	{
		StopSleeping();

		if (burningRope && !timeOut)
		{
			StopRope();
			ropeBurning = false;
		}

		if (GameObject.FindGameObjectWithTag("RoundEnd"))
		{
			GameObject.Find("Board").BroadcastMessage("RoundEndActions");
		}

		if (coinUsed)
		{
			RemoveMana(coinPlayer);
			coinUsed = false;
		}

		currentRoundstoTimeChange--;

		if (currentRoundstoTimeChange <= 0)
		{
			TimeChange();
		}

		//This flips a value between 0 and 1. Why? Because.
		whosTurn = Mathf.Abs(whosTurn + 1 - 2);

		StartRound();
	}

	public void ExpendMana(int manaSpent)
	{
		players[whosTurn].currentMana -= manaSpent;
		UpdateMana();
	}

	void StartRound()
	{
		AddMana();
		DrawCard(whosTurn);
		heroPowerUsed[whosTurn] = false;

		if (boards[whosTurn].GetComponentInChildren<Creature>())
		{
			boards[whosTurn].BroadcastMessage("ResetAttacks");
		}

		players[whosTurn].currentMana = players[whosTurn].maxMana;
		players[whosTurn].usedPower = false;
		roundTime = maxTime;

		if (GameObject.FindGameObjectWithTag("RoundStart"))
		{
			try
			{
				GameObject.Find("Board").BroadcastMessage("RoundStart");
			}
			catch (System.Exception)
			{
				throw;
			}
		}

		if (whosTurn == 1)
		{
			GameObject.Find("Enemy Hand").GetComponent<AI>().AITurn();
		}
		else
		{
			GameObject.Find("YourTurn").GetComponent<YourTurn>().DisplayYourTurn();
		}

		UpdateMana();

		if (whosTurn == 0)
		{
			foreach (Transform child in GameObject.Find("Enemy Playfield").transform)
			{
				child.GetChild(0).GetComponent<Creature>().CanAttack = true;
			}
		}

		CheckForTauntOnField();
	}

	void AddMana()
	{
		if (players[whosTurn].maxMana < 10)
		{
			players[whosTurn].maxMana++;
			players[whosTurn].currentMana++;
			if (whosTurn == 0)
			{
				manaManager.AddMana(whosTurn, players[whosTurn].maxMana);
			}
		}
		UpdateMana();
	}

	void RemoveMana(int playerIndex)
	{
		manaManager.RemoveMana(playerIndex);
		players[playerIndex].maxMana--;
		UpdateMana();
	}

	public void UpdateMana()
	{
		manaTexts[0].text = players[0].currentMana.ToString() + "/" + players[0].maxMana;
		manaTexts[1].text = players[1].currentMana.ToString() + "/" + players[1].maxMana;
		manaManager.UpdateMana(0);
	}

	public void UpdateHealth()
	{
		healthTexts[0].text = players[0].health.ToString();
		healthTexts[1].text = players[1].health.ToString();
	}

	public void DrawCard(int playerId)
	{
		drawManager.CardDraw(playerId);
	}

	public void HeroDamage(int playerIndex, int dmgTaken)
	{
		lastDamage[playerIndex] = dmgTaken;

		players[playerIndex].health -= dmgTaken;
		
		if(players[playerIndex].health > 30)
		{
			players[playerIndex].health = 30;
        }

		UpdateHealth();

		GameObject dDisplay = Instantiate(damageDisplay, heroes[playerIndex].transform, false);
		dDisplay.transform.position += new Vector3(0, 0, -5);
		dDisplay.transform.localRotation = Quaternion.Euler(0f, 200f, 0f);
		dDisplay.GetComponent<DamageDisplay>().SetText(dmgTaken);

		if (players[1].health < 1)
		{
			youWon = true;
			win.SetActive(true);
		}

		if (players[0].health < 1)
		{
			youLost = true;
			lost.SetActive(true);
		}

	}

	public void PlayedCoin(int playerIndex)
	{
		coinPlayer = playerIndex;
		//players[playerIndex].maxMana++;
		//UpdateMana();
		AddMana();
		coinUsed = true;
	}

	public bool UsedHeroPower(int playerIndex)
	{
		return heroPowerUsed[playerIndex];
	}

	public void UseHeroPower(int playerIndex)
	{
		heroPowerUsed[playerIndex] = true;
	}

	void StartRope()
	{
		ropeBurning = true;
		burningRope.GetComponent<RopeHiderScript>().Activate();
	}

	void StopRope()
	{
		ropeBurning = false;
		burningRope.GetComponent<RopeHiderScript>().Deactivate();
	}

	public Player[] Players()
	{
		return players;
	}

	public void StopSleeping()
	{
		if (GameObject.FindGameObjectsWithTag("Sleeping") != null)
		{
			foreach (GameObject g in GameObject.FindGameObjectsWithTag("Sleeping"))
			{
				g.GetComponent<ParticleSystem>().Stop();
			}
		}
	}

	//public int GetNumberOnBoard(int playerIndex)
	//{
	//	return creatureNumOnBoard[playerIndex];
	//}

	public void SetNumberOnBoard(int playerIndex, int change)
	{
		creatureNumOnBoard[playerIndex] += change;
	}

	public int LastDamageTaken(int playerIndex)
	{
		return lastDamage[playerIndex];
	}

	public void SetLastDamage(int playerIndex, int damage)
	{
		lastDamage[playerIndex] = damage;
	}

	public int PlayerTurn
	{
		get
		{
			return whosTurn;
		}
	}

	public int TimeIndex
	{
		get { return timeIndex; }
	}

	public void EndMuligan()
	{
		muliganScript.EndMuligan();
		isPlaying = true;
		GameObject.Find("OnARoll").GetComponent<AudioSource>().Play();
		GameObject.Find("MulliganSong").GetComponent<AudioSource>().Stop();
		StartRound();
	}

	public Creature GetLastAttackedCreature(int playerIndex)
	{
		return lastAttacked[playerIndex];
	}

	public void SetLastAttackedCreature(int playerIndex, Creature creature)
	{
		lastAttacked[playerIndex] = creature;
	}

	public GameObject[] Boards
	{
		get { return boards; }
	}

	public List<int>[] Decks
	{
		get { return decks; }
	}

	public bool IsPlaying
	{
		get { return isPlaying; }
	}

	void TimeChange()
	{
		switch (timeIndex)
		{
			case 0:
				timeIndex++;
				break;

			case 1:
				timeIndex++;
				break;

			case 2:
				timeIndex = 0;
				break;
		}

		if (GameObject.FindGameObjectWithTag("TimeChange"))
		{
			GameObject.Find("Board").BroadcastMessage("TimeChange", timeIndex);
		}
		currentRoundstoTimeChange = roundsBetweenTimeChange;

		string timetext = ("");

		//For llo som fyller på emd chars från timetext
		
		foreach(char c in timeNames[timeIndex])
		{
			timetext += c;
			timetext += ("\n");
		}

		timeTextObject.GetComponent<Text>().text = timetext;

		StartCoroutine(ChangeLight());
	}

	public bool IsSleeping
	{
		get { return isSleeping; }
		set { isSleeping = value; }
	}

	public void UpdateCreatureHp()
	{
		GameObject.Find("Board").BroadcastMessage("UpdateHP");
	}

	IEnumerator ChangeLight()
	{
		dirLight.color = Vector4.Lerp(dirLight.color, timeColors[timeIndex], 1f / (60f * lightTransTime));
		yield return new WaitForFixedUpdate();

		if (dirLight.color != timeColors[timeIndex])
		{
			StartCoroutine(ChangeLight());
		}
	}

	public void CheckForTauntOnField()
	{
		foreach (Transform enemyCreature in GameObject.Find("Enemy Playfield").transform)
		{
			if (enemyCreature.GetChild(0).GetComponent<Creature>().HasTaunt)
			{
				tauntOnField = true;
				break;
			}
			else
			{
				tauntOnField = false;
			}
		}
	}
}
