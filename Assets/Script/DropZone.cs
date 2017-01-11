using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public static int playfieldfCardCount;
	public static int maxCardsOnField = 7;
	GameManager gameManager;

	void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null)
		{
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d != null)
		{
			d.placeHolderParent = this.transform;
		}
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (eventData.pointerDrag == null)
		{
			return;
		}

		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d != null && d.placeHolderParent == this.transform)
		{
			d.placeHolderParent = d.parentToReturnTo;
		}
	}

    public void LowerCardcount()
    {
        playfieldfCardCount--;
    }

	public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d.transform.tag == "Coin" || gameManager.Players()[0].currentMana >= d.transform.GetChild(0).GetComponent<CardClass>().Card.mana)
		{
			if (d != null && playfieldfCardCount < maxCardsOnField && gameManager.PlayerTurn != 1 && d.transform.GetChild(0).GetComponent<CardClass>().OwnerId == 0 || d.transform.tag == "Coin")
			{
				d.parentToReturnTo = this.transform;

				if (!d.playedCard)
				{
                    if (d.transform.tag == "Coin")
                    {
                        d.PlayCard();
                    }
                    else
                    {
                        d.PlayCard();
                        gameManager.ExpendMana(d.transform.GetChild(0).GetComponent<CardClass>().Card.mana);
                        playfieldfCardCount++;
                    }
				}
			}
		}
	}
}