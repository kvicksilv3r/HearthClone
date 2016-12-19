using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class CardGenerator : MonoBehaviour
{
	public string cardPictureName;
	string pictureAssetName;
	[SerializeField]GameObject portrait;
	[SerializeField]GameObject healthTextObj;
	[SerializeField]GameObject damageTextObj;
	[SerializeField]GameObject manaTextObj;
	[SerializeField]GameObject cardTextObj;
	[SerializeField]GameObject cardNameTextObj;
	[SerializeField]GameObject cardRaceTextObj;
	[SerializeField]GameObject cardRaceObj;	
	[SerializeField]GameObject portraitObj;	
	[SerializeField]GameObject dragonObj;
	[SerializeField]GameObject gemObj;
	[SerializeField]GameObject gemHolderObj;
	GameObject card;
	
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Space))
		{
			GenerateCard();
		}
	}

	void GenerateCard()
	{
		pictureAssetName = "Assets/Cards/Textures/" + cardPictureName + ".jpg";
		portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));

		healthTextObj.GetComponent<Text>().text = "15";
		manaTextObj.GetComponent<Text>().text = "10";
		damageTextObj.GetComponent<Text>().text = "5";
		cardTextObj.GetComponent<Text>().text = "<b>Battlecry</b>: Destroy your hero and replace it with Lord Jaraxxus.";
		cardRaceTextObj.GetComponent<Text>().text = "Demon";
		cardNameTextObj.GetComponent<Text>().text = "Lord Jaraxxus";
    }
	
}
