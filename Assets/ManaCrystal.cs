using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ManaCrystal : MonoBehaviour
{

	string fullCrystal;
	string emptyCrystal;
	bool filled = true;

	// Use this for initialization
	void Start()
	{
		fullCrystal = "mana_gem.png";
		emptyCrystal = "mana_gem_empty.png";		
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void FlipMana()
	{
		if (filled)
		{
			GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/"+emptyCrystal, typeof(Texture2D));
			filled = !filled;
		}

		else
		{
			GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/Cards/Textures/"+fullCrystal, typeof(Texture2D));
			filled = !filled;
		}
	}

	public void Reset()
	{
		GetComponent<MeshRenderer>().material.mainTexture = (Texture2D)AssetDatabase.LoadAssetAtPath(fullCrystal, typeof(Texture2D));
		filled = true;

	}


}
