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
	[SerializeField]
	private GameObject tauntShield;

	protected bool timeEffect;

	protected string race;
	protected bool deathrattle, onattack, timechange, endround, startround;

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

		CheckHealth(health);
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

	public bool StartRound
	{
		get { return startround; }
		set { startround = value; }
	}

	public bool AttackAbility
	{
		get { return onattack; }
		set { onattack = value; }
	}

	public bool EndRound
	{
		get { return endround; }
		set { endround = value; }
	}

	public bool TimeChange
	{
		get { return timechange; }
		set { timechange = value; }
	}

	public void GetTaunt()
	{
		hasTaunt = true;
		tauntShield.SetActive(true);
		
		if(ownerId != 0)
		{
			gameManager.CheckForTauntOnField();
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
		if (!dead)
		{
			gameManager.SetLastAttackedCreature(ownerId, this);

			if(damage > health)
			{
				gameManager.SetLastDamage(ownerId, health);
			}
			else
			{
				gameManager.SetLastDamage(ownerId, damage);
			}

			health -= damage;
			UpdateHP();

			GameObject dDisplay = Instantiate(damageDisplay, transform, false);
			dDisplay.GetComponent<DamageDisplay>().SetText(damage);
		}
	}

	public void CheckHealth(int health)
	{
		if (health <= 0)
		{
			currentAttacks = 0;
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

	public void Update()
	{
		UpdateHP();
		UpdateDMG();
	}

	public IEnumerator Death(bool destroyed)
	{
		if (deathrattle && !dead)
		{
			BroadcastMessage("DeathRattle", strength);
		}

		dead = true;

		if(ownerId == 1)
		{
			gameManager.CheckForTauntOnField();
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

