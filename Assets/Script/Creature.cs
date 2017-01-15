using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//By Johanna Pettersson

public class Creature : CardClass
{

	[SerializeField]
	protected int health, strength, maxAttacks, currentAttacks;
	[SerializeField]
	private float waitTime = 2f;

	private bool dead = false;

	protected int[] abilities;

	[SerializeField]
	private GameObject damageDisplay;

	protected bool timeEffect;

	protected string race;
	protected bool deathrattle, onattack;

	[SerializeField]
	protected bool hasTaunt;
	//[SerializeField] private GameObject target;

	public string enemyCreatureTag = "EnemyCreature";
	public string enemyPlayerTag = "EnemyPlayer";

	[SerializeField]
	GameObject deathDisplay;

	GameManager gameManager;

	public Text creatureHPText;
	public Text creatureDMGText;

	void Start()
	{
		creatureHPText.text = health.ToString();
		creatureDMGText.text = strength.ToString();

		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void UpdateHP()
	{
		if (creatureHPText)
			creatureHPText.text = health.ToString();
	}

	public int[] Abilities
	{
		get { return abilities; }
		set { abilities = value; }
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

	public bool IsDead
	{
		get { return dead; }
	}

	public bool Deathrattle
	{
		get { return deathrattle; }
		set { deathrattle = value; }
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

	public string Race
	{
		get { return race; }
		set { race = value; }
	}

	public bool HasTaunt
	{
		get { return hasTaunt; }
		set { hasTaunt = value; }
	}

	public bool TimeEffect
	{
		get { return timeEffect; }
		set { timeEffect = value; }
	}

	public int MaxAttacks
	{
		get { return maxAttacks; }
		set { maxAttacks = value; }
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		UpdateHP();

		GameObject dDisplay = Instantiate(damageDisplay, transform, false);
		dDisplay.GetComponent<DamageDisplay>().SetText(damage);

		CheckHealth(health);
	}

	public void CheckHealth(int health)
	{
		if (health <= 0)
		{
			currentAttacks = 0;
			print(Card.card_name + " ahould be dead, yo");
			health = 0;
			StartCoroutine("Death", false);
		}
		else
		{
			return;
		}
	}

	public void ResetAttacks()
	{
		if (gameManager.PlayerTurn == ownerId)
		{
			currentAttacks = maxAttacks;
			CanAttack = true;
		}
	}

	public IEnumerator Attack()
	{
		yield return new WaitForSeconds(waitTime);
	}

	public IEnumerator Death(bool destroyed)
	{
		dead = true;

		if (deathrattle)
		{
			BroadcastMessage("DeathRattle");
		}

		if (destroyed)
		{
			GameObject dDisplay = Instantiate(deathDisplay, transform, false);
			dDisplay.transform.position = transform.parent.position + new Vector3(0, 3.5f, -1);
		}
		
		yield return new WaitForSeconds(waitTime);

		GameObject.Find("GameManager").GetComponent<GameManager>().SetNumberOnBoard(ownerId, -1);

		Destroy(transform.parent.gameObject);
	}
}

