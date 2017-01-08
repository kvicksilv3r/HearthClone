using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//By Johanna Pettersson

public class Creature : CardClass
{

	[SerializeField]
	protected int health, strength, maxAttacks, currentAttacks;
	[SerializeField]
	private float waitTime;

	protected bool hasTaunt;
	//[SerializeField] private GameObject target;

	public string enemyCreatureTag = "EnemyCreature";
	public string enemyPlayerTag = "EnemyPlayer";

    public Text creatureHPText;
    public Text creatureDMGText;

    void Start()
    {
        creatureHPText.text = health.ToString();
        creatureDMGText.text = strength.ToString();
    }

    public void UpdateHP()
    {
        creatureHPText.text = health.ToString();
    }

    public void UpdateDMG()
    {
        creatureDMGText.text = strength.ToString();
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

    public int CurrentAttacks
    {
        set { currentAttacks = value; }
        get { return currentAttacks; }
    }

    void Combat(GameObject enemy)
	{
		if (canAttack)
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

	public bool HasTaunt
	{
		get { return hasTaunt; }
		set { hasTaunt = value; }
	}

	public int MaxAttacks
	{
		get { return maxAttacks; }
		set { maxAttacks = value; }
	}

	public void TakeDamage(int damage)
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

	public IEnumerator Death()
	{
		print(health);
		yield return new WaitForSeconds(waitTime);
		Destroy(transform.parent.parent.gameObject);
	}

}

