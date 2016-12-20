using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Hero : MonoBehaviour
{
    public string cardPictureName;
    public string heroAbilityName;
    string pictureAssetName;

    [SerializeField]
    GameObject portrait;
    [SerializeField]
    GameObject heroAbilityObj;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateCard();
        }
    }

    void GenerateCard()
    {
        pictureAssetName = "Assets/Models/Heroes/Textures/" + cardPictureName + ".png";
        portrait.GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(pictureAssetName, typeof(Texture2D));

        heroAbilityObj.GetComponent<Text>().text = heroAbilityName;

    }
}
