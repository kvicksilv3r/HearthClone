using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
/* HOW IT WORKS:
 * Use loadFile("file_name.txt",000); method by choosing file_name and card ID [this can be found temporary in the Update() method]
 * In ParseJson() you can get the variables value by writing CardStats.variable.[Example:debug.log(CardStats.health) will give the output: 15]
 */
public class ParseFromJSON : MonoBehaviour {
    string loadJson;
    public class CARDS
    {
        public int card_id;
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
    void ParseJson(string selectedJSON) //string array from StringStorage players card deck
    {   
        CARDS CardStats = new CARDS();
        CardStats = JsonUtility.FromJson<CARDS>(selectedJSON); //parse the jsontable to values for the variables in class CARDS
        Debug.Log(CardStats.card_name);
    }
    void loadFile(string fileName, int ID)
    {
        using (StreamReader sr = new StreamReader(System.IO.Path.Combine(Application.streamingAssetsPath, fileName)))
        {
            List<string> jsonlist = new List<string>();
            while ((loadJson = sr.ReadLine()) != null)
            {
                if (loadJson.Contains("\"card_id\":" + ID.ToString())) // letar efter table i txt-filen
                {
                    Debug.Log("Load table:" + loadJson);
                    ParseJson(loadJson);
                }
            }
        }
    }
    void SendValues()
    {
        //Send CARDS variable values to the file where they rend cards.
    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.C)) //exempel input
        {
            loadFile("Test_json.txt", 15);  //exempel värden
            
        }
    }

}