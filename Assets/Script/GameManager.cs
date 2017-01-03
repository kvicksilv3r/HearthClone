using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	public float roundTime;
	[SerializeField]
	protected float maxTime;
	protected int whosTurn = 0;
	protected bool ropeBurning = false;
	[SerializeField]
	protected GameObject burningRope;
	protected ManaManager manaManager;
	protected int timeIndex = 0; //0 = dawn, 1 = day, 2 = night
	protected int roundsBetweenTimeChange = 2;
	protected int currentRoundstoTimeChange;
	protected int[][] playerDeck = new int[2][]; //??
	protected Creature[,] craturesOnBoaerd = new Creature[2, 7]; //Keeps track of the creatures on the board
	protected Player[] players = new Player[2];

	protected float muliganTime = 15;
	protected bool isPlaying = false;

	protected Text[] manaTexts = new Text[2];
	protected Text[] healthTexts = new Text[2];

	#region LightVars
	protected Light dirLight;
	[SerializeField]
	protected Color[] timeColors;
	[SerializeField]
	protected float lightTransTime;
	#endregion

	void Start()
	{
		//playerDeck[0] = GetComponent<StringStorage>().PlayerDeck;
		//playerDeck[1] = GetComponent<StringStorage>().EnemyDeck;
		players[0] = new Player();
		players[1] = new Player();
		whosTurn = Random.Range(0, 1);
		roundTime = maxTime;
		currentRoundstoTimeChange = roundsBetweenTimeChange;
		dirLight = GameObject.Find("Directional Light").GetComponent<Light>();
		manaManager = GameObject.Find("ManaManager").GetComponent<ManaManager>();
		
	}

	void Update()
	{
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
		if (burningRope && !timeOut)
		{
			StopRope();
			ropeBurning = false;
		}

		currentRoundstoTimeChange--;

		if (currentRoundstoTimeChange <= 0)
		{
			TimeChange();
		}

		whosTurn = Mathf.Abs(whosTurn + 1 - 2);

		StartRound();
	}

	void StartRound()
	{
		if (players[whosTurn].maxMana < 10)
		{
			players[whosTurn].maxMana++;
			manaManager.AddMana(whosTurn, players[whosTurn].maxMana);
		}

		players[whosTurn].currentMana = players[whosTurn].maxMana;
		players[whosTurn].usedPower = false;
		roundTime = maxTime;
	}

	void UpdateMana()
	{
		manaTexts[0].text = players[0].currentMana.ToString();
		manaTexts[1].text = players[1].currentMana.ToString();
	}

	void UpdateHealth()
	{
		healthTexts[0].text = players[0].health.ToString();
		healthTexts[1].text = players[1].health.ToString();
	}

	void DrawCard(int playerId)
	{
		
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

	public Player Players(int playerIndex)
	{
		return players[playerIndex];
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

	void EndMuligan()
	{
		isPlaying = true;
		StartRound();
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

		currentRoundstoTimeChange = roundsBetweenTimeChange;

		StartCoroutine(ChangeLight());
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
}
