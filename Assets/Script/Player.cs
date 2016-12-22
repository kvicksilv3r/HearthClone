using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int health = 30;
	public int maxMana = 0;
	public int currentMana;
	public List<GameObject> cardDeck = new List<GameObject>();
	public bool usedPower = false;
	public int armor;

}
