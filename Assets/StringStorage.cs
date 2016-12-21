using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class StringStorage : MonoBehaviour {
    public string[] Players_Deck; //Players card deck
    void Start () {
       
    }
    void LoadNewDeck()
    {
        Players_Deck = gameObject.GetComponent<DeckCollection>().cards_standard; // Player gets a new deck
    }
    void LoadSavedDeck()
    {
        /*---------Detta får vi ta senare!------------------------*/
        /*string[] examplestring = { "n", "h", "a" };
        //Players_Deck = SaveFile.SavedDeck.Saved_Deck;
        //when we load saved deck, this will convert the deck into a dictionary so it will be easier to modify players_deck.
        var dictionary = examplestring.Select((value, index) => new { value, index }) //the index will be used as ID
                      .ToDictionary(pair => pair.value, pair => pair.index);*/
    }
  
    void addCard(string card) //adds card to Players card deck
    {
        List<string> cardList = new List<string>(Players_Deck);
        Debug.Log("ADD CARD");
        int countItem = cardList.Where(x => x.Equals(card)).Count(); // Counting if there are any duplicates of the selected card in Players_deck
        if (countItem < 2)
        {
            cardList.Add(card);
            Players_Deck = cardList.ToArray();
        }
       
    }
    void removeCard(string card) //removes cars from Plaers card deck
    {
        List<string> cardList2 = new List<string>(Players_Deck);
        cardList2.Remove(card);
        Players_Deck = cardList2.ToArray();
        Debug.Log("Removes card");
    }
    void Update () 
    {
        if (Input.GetKeyDown(KeyCode.W)){ // change input [temporary input]
           string selected_card = gameObject.GetComponent<DeckCollection>().findTable;// hämtar selected table
            addCard(selected_card); // add card to Players_deck
        }
        if (Input.GetKeyDown(KeyCode.A)) // change input [temporary input]
        {
            string selected_card = gameObject.GetComponent<DeckCollection>().findTable; // hämtar selected table
            removeCard(selected_card); // tar bort ett kort från Players card deck
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadNewDeck();
        }
    }

}
