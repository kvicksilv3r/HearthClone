using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YourTurn : MonoBehaviour {

	[SerializeField]
	protected float displayTime;
	[SerializeField]
	protected GameObject turnVisuals;

	public void DisplayYourTurn()
	{
		StartCoroutine("ShowYourTurn");
	}

	IEnumerator ShowYourTurn()
	{
		turnVisuals.SetActive(true);
		yield return new WaitForSeconds(displayTime);
		turnVisuals.SetActive(false);
	}
}
