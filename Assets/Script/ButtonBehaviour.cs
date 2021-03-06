﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//By Johanna Pettersson

public class ButtonBehaviour : MonoBehaviour {
    [SerializeField]
    protected GameObject settingsMenu;

    [SerializeField]
    protected GameObject safeCheck;

    [SerializeField]
    protected GameObject errorMessage;

    [SerializeField]
    protected GameObject optionsMenu;

    [SerializeField]
    protected GameObject[] otherCanvas;

    [SerializeField]
    protected GameObject popUpInfo;

	protected CursorMode cursorMode = CursorMode.Auto;
	protected Vector2 hotSpot = Vector2.zero;


	// Use this for initialization
	void Start()
    {
		Cursor.SetCursor(Resources.Load("Cards/Textures/hand") as Texture2D, hotSpot, cursorMode);
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Cursor.SetCursor(Resources.Load("Cards/Textures/down") as Texture2D, hotSpot, cursorMode);
		}
		if (Input.GetMouseButtonUp(0))
		{
			Cursor.SetCursor(Resources.Load("Cards/Textures/hand") as Texture2D, hotSpot, cursorMode);
		}
	}

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void LoadMyCollection()
    {
        SceneManager.LoadScene("MyCollection");
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene("Cardgames On Motorcycles");
    }

    public void LoadOpenPacks()
    {
        SceneManager.LoadScene("OpenPacks");
    }

    public void LoadChooseDeck()
    {
        SceneManager.LoadScene("ChooseYourDeck");
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void LoadSettings()
    {
        settingsMenu.gameObject.SetActive(!settingsMenu.gameObject.active);
    }

    public void LoadSafeCheck()
    {
        safeCheck.gameObject.SetActive(!safeCheck.gameObject.active);
    }

    public void LoadErrorMessage()
    {
        errorMessage.gameObject.SetActive(!errorMessage.gameObject.active);
    }

    public void ExitAppli()
    {
        Application.Quit();
    }

    public void LoadOptions()
    {
        optionsMenu.gameObject.SetActive(!optionsMenu.gameObject.active);
    }

	public void PriestPic()
	{
		GameObject.Find("ImgPlane").GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Models/Heroes/Textures/hero_priest_oliver") as Texture;
		GameObject.Find("HeroPowerPic").GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Models/Heroes/Textures/hero_priest_oliver") as Texture;
    }

	public void WarlockPic()
	{
		GameObject.Find("ImgPlane").GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Models/Heroes/Textures/hero_warlock_oliver") as Texture;GameObject.Find("ImgPlane").GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Models/Heroes/Textures/hero_warlock_oliver") as Texture;
		GameObject.Find("HeroPowerPic").GetComponent<MeshRenderer>().material.mainTexture = Resources.Load("Models/Heroes/Textures/hero_warlock_oliver") as Texture;
	}


	public void LoadPopUpInfo()
    {
        popUpInfo.gameObject.SetActive(!popUpInfo.gameObject.active);
    }

    public void InactivateCanvases()
    {
        foreach (GameObject canvas in otherCanvas)
        {
            canvas.gameObject.SetActive(!canvas.gameObject.active);
        }
    }

}
