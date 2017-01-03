using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

	public GameObject[,] manaCrystals = new GameObject[2, 10];
	public GameObject[] manaPositions = new GameObject[2];
	public GameObject manaCrystal;
	public GameObject playerManaPos;
	public GameObject enemyManaPos;
	protected GameManager gameManager;

	int test = 1;
	int testmana = 10;

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		manaPositions[0] = playerManaPos;
		manaPositions[1] = enemyManaPos;
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.M))
		{
			AddMana(0, test);
			test++;
		}

		if (Input.GetKeyDown(KeyCode.N))
		{
			ExpendMana(1);
			testmana--;
		}

		if (Input.GetKeyDown(KeyCode.D))
		{
			RemoveMana(0);
		}
	}

	public void AddMana(int playerIndex, int manaCount)
	{
		manaCrystals[playerIndex, manaCount - 1] = Instantiate(manaCrystal, manaPositions[playerIndex].transform.position + new Vector3(2.4f * manaCount - 1, 0, 0), Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;
	}

	public void ResetMana(int playerIndex)
	{
		for (int i = 0; i < manaCrystals.GetLength(playerIndex); i++)
		{
			manaCrystals[playerIndex, i].GetComponent<ManaCrystal>().Reset();
		}
	}

	public void RemoveMana(int playerIndex)
	{
		for (int i = 0; i < 10; i++)
		{
			if (manaCrystals[playerIndex, 9 - i] != null)
			{
				Destroy(manaCrystals[playerIndex, 9-i]);
				break;
			}
		}
	}

	public void ExpendMana(int manaExpended)
	{
		for (int i = 0; i < manaExpended; i++)
		{
			manaCrystals[gameManager.PlayerTurn, gameManager.Players(gameManager.PlayerTurn).currentMana - i - 1].GetComponent<ManaCrystal>().FlipMana();
		}
	}
}
