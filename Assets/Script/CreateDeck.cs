using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
/* TO DO LIST:
 * Vet inte hur vi gör när spelaren ska välja ett kort.
 * Det som behövs är meny när man startar med en ny kortlek eller om man använda en sparad lek. 
 * Det som finns nu är att man ändrar "Selected_Card" och sedan klickar på en W eller A för att lägga till eller ta bort kortet.
 * Man kan få en ny lek eller en sparad lek.
 * */

public class CreateDeck : MonoBehaviour
{
    public int[] Players_Deck; //Players card deck
    public int Selected_Card = 001;
    void Start()
    {

    }
    void LoadNewDeck()
    {
        Players_Deck = new int[] {001, 002, 003, 005, 006, 007, 008, 009, 010 };// Player gets a new deck
    }
    void LoadSavedDeck()
    {
        gameObject.GetComponent<SaveFile>().read();
        Players_Deck = gameObject.GetComponent<SaveFile>().Load_cards;

    }

    void addCard(int cardID) //adds card to Players card deck
    {
        List<int> cardList = new List<int>(Players_Deck);
        Debug.Log("ADD CARD");
        int countItem = cardList.Where(x => x.Equals(cardID)).Count(); // Counting if there are any duplicates of the selected card in Players_deck
        if (countItem < 2)
        {
            cardList.Add(cardID);
            Players_Deck = cardList.ToArray();
        }

    }
    void removeCard(int cardID) //removes cars from Plaers card deck
    {
        List<int> cardList2 = new List<int>(Players_Deck);
        cardList2.Remove(cardID);
        Players_Deck = cardList2.ToArray();
        Debug.Log("Removes card");
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        { 
            addCard(Selected_Card); // add card to Players_deck
        }
        if (Input.GetKeyDown(KeyCode.A)) // change input [temporary input]
        {
          
            removeCard(Selected_Card); // tar bort ett kort från Players card deck
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadSavedDeck();
        }
        
    }

}
