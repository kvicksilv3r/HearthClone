using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndStuff : MonoBehaviour
{

	GameManager gameManager;
	Creature parent;
	[SerializeField]
	protected GameObject creaturePrefab;


	public void RoundEndActions()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		parent = transform.parent.GetComponent<Creature>();

		foreach (int effect in transform.parent.GetComponent<Creature>().Abilities)
		{
			if (effect == 12 && parent.IsDead == false)
			{
				if (gameManager.TimeIndex == 2)
				{
					DarkAcolyteAbility();
				}
			}
		}
	}

	void SpawnImp()
	{
		GameObject g;
		g = Instantiate(creaturePrefab, transform.parent.parent.parent.transform, false);
		g.transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(999);

		if (transform.parent.GetComponent<Creature>().OwnerId == 1)
		{
			g.transform.rotation = new Quaternion(0, 0, 0, 180);
		}
		g.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 171);
		g.transform.position += new Vector3(0, 0, -1);
		g.GetComponent<Draggable>().PlayCard();
	}

	void DarkAcolyteAbility()
	{
		if (gameManager.Boards[Mathf.Abs(parent.OwnerId + 1 - 2)].transform.childCount > 0)
		{
			if (Random.Range(0, 2) == 1)
			{
				gameManager.HeroDamage(Mathf.Abs(parent.OwnerId + 1 - 2), 1);
			}
			else
			{
				gameManager.Boards[Mathf.Abs(parent.OwnerId + 1 - 2)].transform.GetComponentsInChildren<Creature>()[Random.Range(0, gameManager.Boards[Mathf.Abs(parent.OwnerId + 1 - 2)].transform.childCount)].TakeDamage(1);
			}
		}
		else
		{
			gameManager.HeroDamage(Mathf.Abs(parent.OwnerId + 1 - 2), 1);
		}
	}
}
