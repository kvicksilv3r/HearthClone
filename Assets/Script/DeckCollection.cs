using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class DeckCollection : MonoBehaviour {
    public string[] cards_library;
    public string[] cards_standard;
    public int SelectedCard = 001; //[exempel:] den kommer att ändras beroende på vilket kort som har blivit selectad i en annan fil. 
    public string findTable;
    //all cards here
    // Use this for initialization
    void Start() {

    }
    void Awake()
    {
        Dictionary<int, string> LibraryCards = new Dictionary<int, string>();
        LibraryCards.Add(001, "{\"ID\":1, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(002, "{\"ID\":2, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(003, "{\"ID\":3, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(004, "{\"ID\":4, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(005, "{\"ID\":5, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(006, "{\"ID\":6, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(007, "{\"ID\":7, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        LibraryCards.Add(008, "{\"ID\":8, \"HERO_POWER_ID\":3, \"NOTE_MINI_GUID\":\"exampl2\", \"CRAFTING_EVENT\":\"example3\" }");
        cards_library = LibraryCards.Values.ToArray();
        findTable = LibraryCards.FirstOrDefault(x => x.Key == SelectedCard).Value;
        

        List<string> StandardCards = new List<string>(cards_standard);
        StandardCards.Add("{\"ID\":001, \"Name\":Berzerker, \"Mana\":\"3\", \"Health\":\"3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":example.jpg,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":002, \"Name\":Wolf Warriors \"Mana\":\"4\", \"Health\":\"3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":003, \"Name\":Moonlight Assain, \"Mana\":\"1\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":004, \"Name\":Scout, \"Mana\":\"2\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":005, \"Name\":Shield Maiden, \"Mana\":\"5\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":006, \"Name\":The Lawgiver, \"Mana\":\"5\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":007, \"Name\":Tamed Beast, \"Mana\":\"6\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        StandardCards.Add("{\"ID\":008, \"Name\":Augur, \"Mana\":\"4\", \"Health\":\"example3\", \"Description\":1,\"Damage\":1,\"card_type\":1, \"Rarity\":1,\"picture_name\":1,\"class_name\":1,\"race\":1,\"abilities_cr\":1,\"abilities_sp\":1,\"day_or_night\":1,\"flavor_text\":1, }");
        cards_standard = StandardCards.ToArray();

    }

    // Update is called once per frame
    void Update () {

    
    }

}