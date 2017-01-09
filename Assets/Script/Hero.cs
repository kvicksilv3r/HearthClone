using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public string cardPictureName;
    public string heroAbilityName;
    string pictureAssetName;
    public static bool priest;
    public static bool warlock;

    [SerializeField]
    GameObject portrait;
    [SerializeField]
    GameObject heroAbilityObj;
    [SerializeField]
    GameObject dragAbility;
    [SerializeField]
    GameObject pressAbility;

    void Start()
    {
        dragAbility.SetActive(false);
        pressAbility.SetActive(false);
		GenerateHero();
    }

    public void GenerateHero()
    {
        pictureAssetName = "Assets/Models/Heroes/Textures/" + cardPictureName + ".png";
        portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));

        heroAbilityObj.GetComponent<Text>().text = heroAbilityName;

        if(cardPictureName == "Priest")
        {
            priest = true;
            dragAbility.SetActive(true);
        }

        if (cardPictureName == "Warlock")
        {
            warlock = true;
            //pressAbility.SetActive(true);
        }
    }
}
