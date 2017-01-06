using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class SpellCardGenerator : MonoBehaviour
{

    public string cardPictureName;
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
    GameObject gemObj;
    [SerializeField]
    GameObject gemHolderObj;
    [SerializeField]
    GameObject cardFaceObj;
    GameObject card;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
          //  GenerateCard(0);
        }
    }

    public void GenerateCard(CARDS c)
    {
        pictureAssetName = "Assets/Cards/Textures/" + cardPictureName + ".png";
        portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));

        manaTextObj.GetComponent<Text>().text = "10";
        cardTextObj.GetComponent<Text>().text = "Deal 10 Damage.";
        cardNameTextObj.GetComponent<Text>().text = "Pyroblast";

        gemHolderObj.SetActive(true);
        gemObj.SetActive(true);

        cardFaceObj.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/Cardfronts/card_spell_neutral.png", typeof(Texture2D));
        transform.GetComponent<CardClass>().CardName = cardNameTextObj.GetComponent<Text>().text;
    }

}
