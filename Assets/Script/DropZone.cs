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

	public void OnDrop(PointerEventData eventData)
	{
		Draggable d = eventData.pointerDrag.GetComponent<Draggable>();

		if (d.transform.tag == "Coin" || gameManager.Players()[0].currentMana >= d.transform.GetChild(0).GetComponent<CardClass>().Card.mana)
		{
			if (d != null && gameManager.Boards[0].transform.GetComponentsInChildren<Creature>().Length < maxCardsOnField && gameManager.PlayerTurn != 1 && d.transform.GetChild(0).GetComponent<CardClass>().OwnerId == 0 || d.transform.tag == "Coin")
			{
				d.parentToReturnTo = this.transform;

				if (!d.playedCard)
				{
					d.PlayCard();
					if (d.transform.tag != "Coin")
					{
						gameManager.ExpendMana(d.transform.GetChild(0).GetComponent<CardClass>().Card.mana);
						gameManager.SetNumberOnBoard(d.transform.GetChild(0).GetComponent<CardClass>().OwnerId, 1);
					}
				}
			}
		}
	}
}