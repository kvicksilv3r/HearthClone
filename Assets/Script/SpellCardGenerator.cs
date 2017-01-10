using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SpellCardGenerator : MonoBehaviour
{

	public string cardPictureName;
	protected Spell spell;
	protected GameManager gameManager;
	string pictureAssetName;
	[SerializeField]
	GameObject portrait;
	
	[SerializeField]
	GameObject manaTextObj;
	[SerializeField]
	GameObject cardTextObj;
	[SerializeField]
	GameObject cardNameTextObj;
	[SerializeField]
	GameObject portraitObj;
	[SerializeField]
	GameObject gemObj;
	[SerializeField]
	GameObject gemHolderObj;
	[SerializeField]
	GameObject cardFaceObj;
	ParseFromJSON json;
	CARDS c;

	void Start()
	{
		json = GameObject.Find("GameManager").GetComponent<ParseFromJSON>();
	}

    public void GenerateCard(CARDS card)
    {
		c = card;

		spell = GetComponent<Spell>();
		GetComponent<CardClass>().Card = card;

		pictureAssetName = "Assets/Cards/Textures/" + c.picture_name + ".png";
		portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));
		
		manaTextObj.GetComponent<Text>().text = c.mana.ToString();
		cardTextObj.GetComponent<Text>().text = c.description;
		cardNameTextObj.GetComponent<Text>().text = c.card_name;

		gemHolderObj.SetActive(true);
        gemObj.SetActive(true);

		gemObj.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/Gems/gem_" + c.rarity + ".png", typeof(Texture2D));

		cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/Cardfronts/card_spell_" + c.class_name + ".png", typeof(Texture2D));
		transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
	}

}
