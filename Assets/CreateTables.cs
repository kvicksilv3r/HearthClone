using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParseToJSON : MonoBehaviour {

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
    void Start()
    {

    }

    void ParseCardToJson() //values from another  file 
    {
        Debug.Log("JOSN TEEST ");
        //jsontable for cards
        CARDS cardDeck = new CARDS();
         cardDeck.card_name = "Lord Jaraxxus";
         cardDeck.mana = 10;
         cardDeck.health=15;
         cardDeck.description = "Burning legion lel";
         cardDeck.damage =5;
         cardDeck.card_type = "spell";
         cardDeck.rarity = "legendary";
         cardDeck.picture_name = "jaraxxus";
         cardDeck.class_name = "warlock";
         cardDeck.race = "Demon";
         cardDeck.abilities_cr = new int[] {1, 3, 4};
         cardDeck.abilities_sp = new int[] {};
         cardDeck.flavor_text = "Le burning lord of the legion xdd";
         cardDeck.day_or_night = "none";
         string json2 = JsonUtility.ToJson(cardDeck);
         Debug.Log(json2); //print table

    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ParseCardToJson();
        }

    }
}
