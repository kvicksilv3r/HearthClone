using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//By Johanna Pettersson

public class WeaponBehaviour : CardClass {

    [SerializeField]
    protected int damage, maxDurability;

    public int durability;

    int Durability
    {
        set { durability = value; }
        get { return durability; }
    }



    // Use this for initialization
    void Start () {

        PlayerBehaviour player = GetComponent<PlayerBehaviour>();
        player.Strength = damage;
        durability = maxDurability;
        player.CanAttack = true;
		
	}

    public void CheckDurability (int durability)
    {
        if(durability <= 0)
        {
            //No more attacks
            Destroy(this.gameObject);
        }
        else
        {
            return;
        }
    }

	

}
