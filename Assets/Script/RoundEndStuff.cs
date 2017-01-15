using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundEndStuff : MonoBehaviour {

	public int effect;
	GameManager gameManager;
	[SerializeField]
	protected GameObject creaturePrefab;

	// Use this for initialization
	void Start () {
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void RoundEndActions()
	{
		if(effect == 12)
		{
			if(gameManager.GetNumberOnBoard(transform.parent.GetComponent<Creature>().OwnerId) < 7)
			{
				if(gameManager.TimeIndex == 2)
					SpawnImp();
			}
		}
	}

	void SpawnImp()
	{
		GameObject g;
		g = Instantiate(creaturePrefab, transform.parent.parent.parent.transform, false);
		g.transform.GetChild(0).GetComponent<CardGenerator>().GenerateCard(999);
		

		if(transform.parent.GetComponent<Creature>().OwnerId == 1)
		{
			g.transform.rotation = new Quaternion(0, 0, 0, 180);
		}
		g.GetComponent<RectTransform>().sizeDelta = new Vector2(150, 171);
		g.transform.position += new Vector3(0, 0, -1);
		g.GetComponent<Draggable>().PlayCard();
    }
}
