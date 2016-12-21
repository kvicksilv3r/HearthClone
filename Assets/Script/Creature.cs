﻿using UnityEngine;
using System.Collections;

//By Johanna Pettersson

public class Creature : CardClass
{

    [SerializeField] protected int health;
    [SerializeField] private float waitTime;

    public int strength;
    //[SerializeField] private GameObject target;

    public string enemyCreatureTag = "EnemyCreature";
    public string enemyPlayerTag = "EnemyPlayer";

    public int Health
    {
        set { health = value; }
        get { return health; }
    }

    public int Strength
    {
        set { strength = value; }
        get { return strength;  }
    }

    void Combat(GameObject enemy)
    {
        if(canAttack)
        {
            if (enemy != null && enemy.tag == enemyCreatureTag) // if(target != null && target != ally && target == creature)
            {
                Creature currentenemy = enemy.GetComponent<Creature>();
                currentenemy.TakeDamage(strength);
                TakeDamage(currentenemy.strength);
                canAttack = false;
            }
            else if (enemy != null && enemy.tag == enemyPlayerTag) //if(target != null && target != owner && target == player)
            {
                canAttack = false;
            }
            else
            {
                return;
            }
        }
        else
        {
            //Can not attack.
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        CheckHealth(health);
    }

    public void CheckHealth(int health)
    {
        if(health <= 0)
        {
            health = 0;
            Death();
        }
        else
        {
            return;
        }
    }
    
    public IEnumerator Death()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
    }

}
