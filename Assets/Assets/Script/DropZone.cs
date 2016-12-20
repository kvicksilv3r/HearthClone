﻿using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
	public static int playfieldfCardCount;
	public static int maxCardsOnField = 7;

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

		if (d != null && playfieldfCardCount < maxCardsOnField)
		{
			d.parentToReturnTo = this.transform;

			if (!d.playedCard)
			{				
				d.PlayCard();
			}
			playfieldfCardCount++;
		}
	}
}