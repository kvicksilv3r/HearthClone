using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

public class ParseFromJSON : MonoBehaviour {
    string fileName = "Test_json.txt";
    string selected_json;
    string[] jsonarray;
    string loadJson;
    public class CARDS
    {
        public string card_name;
        public int mana;
        public int health;
        public string description;
        public int damage;
        public string card_type;
        public string rarity;
        public string picture_name;
        public string class_name;
        public string race;
        public int[] abilities_cr;
        public int[] abilities_sp;
        public string flavor_text;
        public string day_or_night;
    }
    // Use this for initialization
    void Start () {

    }
    void ParseJson(string[] jsonString) //string array from StringStorage players card deck
    {
        selected_json = "card_name\":\"Lord Jaraxxus"; //[EXAMPLE] selected_json should change value. maybe we should get an ID to the tables
        CARDS CardStats = new CARDS();
        var index = Array.FindIndex(jsonString, row => row.Contains(selected_json));
        Debug.Log("index" + index);
        CardStats = JsonUtility.FromJson<CARDS>(jsonString[index]); //parse the jsontable to values for the variables in class CARDS
        Debug.Log(CardStats.card_name);
    }
    void SendValues()
    {
        //Send CARDS variable values to the file where they rend cards.
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C))
        {
             using (StreamReader sr = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, fileName)))
            {
                List<string> jsonlist = new List<string>();
                while ((loadJson = sr.ReadLine()) != null)
                {
                    jsonlist.Add(loadJson);
                }
                
                jsonarray = jsonlist.ToArray();
                
                ParseJson(jsonarray);
            }
            
        }
    }

}