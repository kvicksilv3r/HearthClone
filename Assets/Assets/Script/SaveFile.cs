using UnityEngine;
using System.Collections;
using System;
using System.IO;
using System.Collections.Generic; /*test*/
using System.Linq;

public class SaveFile : MonoBehaviour
{

    /*GUI WINDOW*/
    public Rect windowRect = new Rect(0, 0, Screen.width, Screen.height);
    private bool showWindow = true;
    /*saving data*/
    public string filePath_cards;
    public string filePath_gameData;
    /*test string array*/
    public string[] cards;
    public string[] gameData;
    public string[] Load_gameData;
    public string[] Load_cards;


    void Start()
    {
        filePath_cards = System.IO.Path.Combine(Application.streamingAssetsPath, "SavedCards"); //gets the path for the streamingAssets folder where there is a file called "SavedData".
        filePath_gameData = System.IO.Path.Combine(Application.streamingAssetsPath, "SavedData");
        windowRect = new Rect(0, 0, Screen.width, Screen.height); //GUI 

    }
    /*GUI */
    void OnGUI()
    {
        if (showWindow == true)
        {
            windowRect = GUI.Window(1, windowRect, DoMyWindow, "Save");

        }
    }
    void DoMyWindow(int windowID)
    {
        if (windowID == 1)
        {
            if (GUI.Button(new Rect(250, 200, 100, 20), "Save"))
            {
                showWindow = false;
                save();
            }
            if (GUI.Button(new Rect(250, 200, 100, 20), "Read"))
            {
                read();
            }
        }

    }
    
/*For saving data*/
    void save()
    {/*
        //creates or replace a text file and add every element from stringFile as new line of text.
        using (StreamWriter sw = new StreamWriter(filePath_cards))
        {
            cards = gameObject.GetComponent<StringStorage>().Players_Deck;
            for (int i = 0; i < cards.GetLongLength(0); i++)
            {
                sw.WriteLine(cards[i]);
            }

        }
        using (StreamWriter sw = new StreamWriter(filePath_gameData))
        {
            gameData = gameObject.GetComponent<ParseToJSON>().parsed_gameData;

            for (int i = 0; i < cards.GetLongLength(0); i++)
            {
                sw.WriteLine(gameData[i]);
            }

        }*/
    }
    /*reads files*/
    void read()
    {
        using (StreamReader sr = new StreamReader(filePath_cards))
        {
            List<string> cardDataList = new List<string>(Load_cards);
            while (sr.Peek() >= 0)
            {
                cardDataList.Add(sr.ReadLine()); //adds one line at a time to datalist 
            }
            Load_cards = cardDataList.ToArray(); //converts the list to an string array 
            for (int i = 0; i < Load_cards.GetLength(0); i++)
            {
                Debug.Log("Load_cards:" + Load_cards[i]);
            }
        }
        using (StreamReader sr = new StreamReader(filePath_gameData))
        {
            List<string> gameDataList = new List<string>(Load_gameData);
            while (sr.Peek() >= 0)
            {
                gameDataList.Add(sr.ReadLine()); //adds one line at a time to datalist 
            }
            Load_gameData = gameDataList.ToArray(); //converts the list to an string array 
            for (int i = 0; i < Load_gameData.GetLength(0); i++)
            {
                Debug.Log("Load_gameData:" + Load_gameData[i]);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {

    }
}


