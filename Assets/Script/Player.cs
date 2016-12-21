using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public int health = 30;
	public int maxMana = 1;
	public List<GameObject> cards = new List<GameObject>();
	public bool usedPower = false;

}
