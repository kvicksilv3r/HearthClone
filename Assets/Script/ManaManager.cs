using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaManager : MonoBehaviour
{

	public GameObject[,] manaCrystals = new GameObject[2, 11];
	public GameObject[] manaPositions = new GameObject[2];
	public GameObject manaCrystal;
	public GameObject playerManaPos;
	public GameObject enemyManaPos;
	protected GameManager gameManager;

	// Use this for initialization
	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		manaPositions[0] = playerManaPos;
		manaPositions[1] = enemyManaPos;
	}

	public void AddMana(int playerIndex, int manaCount)
	{
		manaCrystals[playerIndex, manaCount - 1] = Instantiate(manaCrystal, manaPositions[playerIndex].transform.position + new Vector3(2.4f * manaCount - 1, 0, 0), Quaternion.Euler(-90, 0, 0), this.transform) as GameObject;
		UpdateMana(playerIndex);
	}

	public void ResetMana(int playerIndex)
	{
		for (int i = 0; i < gameManager.Players()[playerIndex].maxMana; i++)
		{
			manaCrystals[playerIndex, i].GetComponent<ManaCrystal>().Reset();
		}
	}

	public void RemoveMana(int playerIndex)
	{
		for (int i = 0; i < 11; i++)
		{
			if (manaCrystals[playerIndex, 10 - i] != null)
			{
				Destroy(manaCrystals[playerIndex, 10 - i]);
				break;
			}
		}
	}

	public void UpdateMana(int playerIndex)
	{
		ResetMana(playerIndex);
		int manaDif = gameManager.Players()[playerIndex].maxMana - gameManager.Players()[playerIndex].currentMana;

        for (int i = 0; i < manaDif; i++)
		{
			manaCrystals[playerIndex, gameManager.Players()[playerIndex].maxMana - i - 1].GetComponent<ManaCrystal>().EmptyMana();
		}
	}
}
