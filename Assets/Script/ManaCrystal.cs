using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManaCrystal : MonoBehaviour
{

	string fullCrystal = "mana_gem.png";
	string emptyCrystal = "mana_gem_empty.png";
	string assetPath = "Assets/Cards/Textures/";

	// Use this for initialization
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void EmptyMana()
	{
		GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath + emptyCrystal, typeof(Texture2D));
	}

	public void Reset()
	{
		GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(assetPath + fullCrystal, typeof(Texture2D));
	}


}
