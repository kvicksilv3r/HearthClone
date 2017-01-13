using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ManaCrystal : MonoBehaviour
{

	string fullCrystal = "mana_gem";
	string emptyCrystal = "mana_gem_empty";
	string assetPath = "Cards/Textures/";

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
		GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(assetPath + emptyCrystal) as Texture;
	}

	public void Reset()
	{
		GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(assetPath + fullCrystal) as Texture;
	}


}
