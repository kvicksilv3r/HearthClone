using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Johanna Pettersson

public class PlayerBehaviour : MonoBehaviour
{
    protected int health, strength;

    [SerializeField]
    private float waitTime;

    [SerializeField]
    private string playerName;

    protected bool canAttack;



    public bool CanAttack
    {
        set { canAttack = value; }
        get { return canAttack; }
    }

    public int Health
    {
        set { health = value; }
        get { return health; }
    }

    public int Strength
    {
        set { strength = value; }
        get { return strength; }
    }


    //NOTICE ME SENPAI
    void Start()
    {
        canAttack = false;
    }
    

    void Combat(GameObject enemy)
    {
        if (canAttack)
        {
            PlayerBehaviour currentPlayerEnemy = enemy.GetComponent<PlayerBehaviour>();
            Creature currentCreatureEnemy = enemy.GetComponent<Creature>();
            WeaponBehaviour weapon = GetComponent<WeaponBehaviour>();

            if (enemy != null && enemy == (gameObject.GetComponent("Creature") as Creature) /*ID-tag*/)
            {
                weapon.durability -= 1;
                currentCreatureEnemy.TakeDamage(strength);
                TakeDamage(currentCreatureEnemy.strength);
                weapon.CheckDurability(weapon.durability);
                canAttack = false;
            }
            else if (enemy != null && enemy == (gameObject.GetComponent("PlayerBehaviour") as PlayerBehaviour) /*ID-tag*/)
            {
                weapon.durability -= 1;
                currentPlayerEnemy.TakeDamage(strength);
                TakeDamage(currentPlayerEnemy.strength);
                weapon.CheckDurability(weapon.durability);
                canAttack = false;
            }
            else
            {
                //Need a valid target
                return;
            }
        }
        else
        {
            //Can not attack.
        }

    }

    void TakeDamage(int damage)
    {
        health -= damage;
        CheckHealth(health);
    }

    public void CheckHealth(int health)
    {
        if (health <= 0)
        {
            health = 0;
            Death();
        }
        else
        {
            return;
        }
    }

    void EquipWeapon()
    {
        Draggable drag = GetComponent<Draggable>();
        drag.enabled = true;
    }

    public IEnumerator Death()
    {
        yield return new WaitForSeconds(waitTime);
        Destroy(this.gameObject);
        //GAMEOVER
    }
}
