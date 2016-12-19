using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Hand : MonoBehaviour, IDropHandler//, IPointerEnterHandler, IPointerExitHandler
{
    public int cardsInHands;
    public int maxCardsInHands = 10;
    public HorizontalLayoutGroup spacing;
    public int startSpacing = 200;


    void Update()
    {
        cardsInHands = this.transform.childCount;

        spacing.spacing = startSpacing - (5 * cardsInHands);
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (cardsInHands > maxCardsInHands)
        {
            Destroy(this.transform.GetChild(this.transform.childCount - 1).gameObject);
        }
    }
}
