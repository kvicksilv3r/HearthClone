using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float roundTime;
	[SerializeField]
	protected float maxTime;
	protected int whosTurn = 0;
	protected bool ropeBurning = false;
	[SerializeField]
	protected GameObject burningRope;
	protected int[][] playerDeck = new int[2][];
	protected Player[] players = new Player[2];

	void Start()
	{
		//playerDeck[0] = GetComponent<StringStorage>().PlayerDeck;
		//playerDeck[1] = GetComponent<StringStorage>().EnemyDeck;
		roundTime = maxTime;
	}

	void Update()
	{
		roundTime -= Time.deltaTime;

		if(roundTime <= 20 && !ropeBurning)
		{
			StartRope();
		}

		if(roundTime <= 0)
		{
			NextRound();
		}
	}

	public void NextRound()
	{
		roundTime = maxTime;
		if (burningRope)
		{
			StopRope();
			ropeBurning = false;
		}
		whosTurn = Mathf.Abs(whosTurn + 1 - 2);
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
		burningRope.GetComponent<RopeHiderScript>().Deactivate();
	}

	public int PlayerTurn
	{
		get
		{
			return whosTurn;
		}
	}
}
