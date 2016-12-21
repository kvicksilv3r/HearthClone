using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//By Johanna Pettersson

public class CardClass : MonoBehaviour
{

    [SerializeField]
    protected int rarity;

    [SerializeField]
    protected GameObject[] gems;

    [SerializeField]
    protected GameObject gem;

    [SerializeField]
    protected string cardName, cardDescription;

    [SerializeField]
    protected int cardCost;

    public Abilities[] ability;

    protected bool canAttack;

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

        switch (rarity)
        {
            case 0:
                break;
            case 1:
                gem = gems[0];
                break;
            case 2:
                gem = gems[1];
                break;
            case 3:
                gem = gems[2];
                break;
            case 4:
                gem = gems[3];
                break;

            case 5:
                gem = gems[4];
                break;

            default:
                break;

        }

        gem.gameObject.SetActive(true);

    }

    void SetTarget()
    {

    }
}
