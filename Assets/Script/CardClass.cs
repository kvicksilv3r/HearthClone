using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//By Johanna Pettersson

public class CardClass : MonoBehaviour
{

    [SerializeField]
    protected int rarity;

    [SerializeField]
    protected string cardType;

    [SerializeField]
    protected GameObject[] gems;

    [SerializeField]
    protected GameObject gem;

    [SerializeField]
    protected string cardName, cardDescription;

    [SerializeField]
    protected int cardCost;

    public Abilities[] ability;

	[SerializeField]
	protected bool canAttack;

	[SerializeField]
	protected bool canTargetFriendly;

	[SerializeField]
	protected int ownerId;

	public int OwnerId
	{
		get { return ownerId; }
        set { ownerId = value; }
	}

    public string CardType
    {
        get { return cardType; }
        set { cardType = value; }
    }

    public bool CanTargetFriendly
	{
		get { return canTargetFriendly; }
		set { canTargetFriendly = value; }
	}

    public bool CanAttack
    {
        set { canAttack = value; }
        get { return canAttack;  }
    }

    public int CardCost
    {
        set { cardCost = value; }
        get { return cardCost; }
    }

    public string CardName
    {
        set { cardName = value; }
        get { return cardName; }
    }

    // Use this for initialization
    void Start()
    {
        //Here would be awesome if we could set the current player as the owner.
		
    }

    void SetTarget()
    {

    }
}
