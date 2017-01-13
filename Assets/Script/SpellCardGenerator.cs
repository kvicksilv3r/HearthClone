using UnityEngine;
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

		pictureAssetName = "Cards/Textures/" + c.picture_name;
		portrait.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(pictureAssetName) as Texture;

		manaTextObj.GetComponent<Text>().text = c.mana.ToString();
		cardTextObj.GetComponent<Text>().text = c.description;
		cardNameTextObj.GetComponent<Text>().text = c.card_name;

		gemHolderObj.SetActive(true);
        gemObj.SetActive(true);

		gemObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Gems/gem_" + c.rarity) as Texture;

		cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Cards/Textures/Cardfronts/card_spell_" + c.class_name) as Texture;
		transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
	}

}
