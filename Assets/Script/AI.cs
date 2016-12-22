using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour
{

    public List<GameObject> HandList = new List<GameObject>();

    void AITurn()
    {
        foreach (Transform child in transform)
        {
            GameObject[] creaturesInHand = GameObject.FindGameObjectsWithTag("Creature");
            GameObject[] spellsInHand = GameObject.FindGameObjectsWithTag("Spell");

            if (child.tag == "Creature")
            {
                CheckCurrentMana();
                
                //if(currentMana >= creatureMana)
                //{
                    PlayCreature();
                //}
            }

            if(child.tag == "Spell")
            {
                CheckCurrentMana();

                //if(currentMana >= spellMana)
                //{
                PlayCreature();
                //}
            }
        }
    }

    void CheckCurrentMana()
    {

    }
		
    void PlayCreature()
    {

    }
    
}
