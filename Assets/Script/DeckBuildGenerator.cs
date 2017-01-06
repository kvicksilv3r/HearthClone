using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckBuildGenerator : MonoBehaviour {

	public GameObject[] cards = new GameObject[8];
	int generatorIndex = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.G))
		{
			GenerateCards();
			generatorIndex++;
		}
	}

	void GenerateCards()
	{
		for(int i =0; i< cards.Length; i++)
		{
			//cards[i].GetComponent<CardGenerator>().GenerateCard(i + (generatorIndex * 8) +1);
		}
	}
}
