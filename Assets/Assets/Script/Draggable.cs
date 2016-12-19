using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

	public Transform parentToReturnTo = null;
	public Transform placeHolderParent = null;
	Vector3 originalScale;
	public GameObject cardBackground;
	public GameObject placeHolder = null;
	public LayoutElement le;
    public bool playedCard = false;
	public static bool dragging;
	public LayerMask lMask;

	void Start()
	{
		originalScale = this.transform.GetChild(0).localScale;
		dragging = false;
		//    panelXpos = this.transform.GetChild(1).position.x;
		//    panelYpos = this.transform.GetChild(1).position.y;
		//    showCardInfoUI = (GameObject)Instantiate(gameObject, new Vector3(panelXpos,panelYpos), Quaternion.identity);
		//    showCardInfoUI.transform.parent = this.transform.GetChild(1);
		//    showCardInfoUI.transform.localScale += new Vector3(0.5F, 0, 0);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		dragging = true;

		//this.transform.GetChild(0).localScale = originalScale;
		this.transform.GetChild(0).position = this.transform.position;

		if (playedCard != true)
		{
			placeHolder = new GameObject();
			placeHolder.transform.SetParent(this.transform.parent);
			placeHolder.transform.position = transform.position;

			le = placeHolder.AddComponent<LayoutElement>();

			le.preferredWidth = this.transform.GetComponent<LayoutElement>().preferredWidth;
			le.preferredHeight = this.transform.GetComponent<LayoutElement>().preferredHeight;
			le.flexibleWidth = 0;
			le.flexibleHeight = 0;

			placeHolder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

			parentToReturnTo = this.transform.parent;

			placeHolderParent = parentToReturnTo;

			this.transform.SetParent(this.transform.parent.parent);

			GetComponent<CanvasGroup>().blocksRaycasts = false;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{
		if (!playedCard)
		{
			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			Physics.Raycast(ray, out hit, 1000, lMask);

			transform.position = hit.point + new Vector3(0, 0, -0.001f);
			//this.transform.position = eventData.position;
			//transform.GetChild(0).transform.position = new Vector3(transform.GetChild(0).transform.position.x, transform.GetChild(0).transform.position.y, -20);

			if (placeHolder.transform.parent != placeHolderParent && DropZone.playfieldfCardCount < DropZone.maxCardsOnField)
			{
				placeHolder.transform.SetParent(placeHolderParent);
			}

			int newSiblingindex = placeHolderParent.childCount;

			for (int i = 0; i < placeHolderParent.childCount; i++)
			{
				if (this.transform.position.x < placeHolderParent.transform.GetChild(i).position.x)
				{
					newSiblingindex = i;

					if (placeHolder.transform.GetSiblingIndex() < newSiblingindex)
					{
						newSiblingindex--;
					}
					break;
				}
			}
			placeHolder.transform.SetSiblingIndex(newSiblingindex);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		dragging = false;

		this.transform.GetChild(0).localScale = originalScale;

		transform.position = new Vector3(transform.position.x, transform.position.y, -1);
		this.transform.SetParent(parentToReturnTo);

		if (placeHolder != null)
		{
			this.transform.SetSiblingIndex(placeHolder.transform.GetSiblingIndex());
		}

		if (playedCard == true)
		{
			cardBackground.SetActive(false);
		}

		GetComponent<CanvasGroup>().blocksRaycasts = true;

		Destroy(placeHolder);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		if (!playedCard && !dragging)
		{
			this.transform.GetChild(0).position = transform.position + new Vector3(0, 10f, -20);
			this.transform.GetChild(0).localScale = originalScale * 1.5f;
		}

		//showCardInfoPanel.SetActive(true);
		//showCardInfoUI.SetActive(true);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		if (!dragging)
		{
			this.transform.GetChild(0).position = this.transform.position;
			this.transform.GetChild(0).localScale = originalScale;
		}
		//showCardInfoPanel.SetActive(false);
		//showCardInfoUI.SetActive(false);
	}
}