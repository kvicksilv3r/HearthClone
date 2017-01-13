using UnityEngine;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public string cardPictureName;
    public string heroAbilityName;
    string pictureAssetName;
    public static bool priest;
    public static bool warlock;
	public int playerId;
	

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
        pictureAssetName = "Models/Heroes/Textures/" + cardPictureName;
        portrait.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(pictureAssetName) as Texture2D;

        heroAbilityObj.GetComponent<Text>().text = heroAbilityName;

        if (transform.gameObject == GameObject.Find("Hero"))
        {
            playerId = 0;
        }
        
        if(transform.gameObject == GameObject.Find("Enemy Hero"))
        {
            playerId = 1;
        }

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
