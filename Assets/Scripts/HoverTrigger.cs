using UnityEngine;
using UnityEngine.EventSystems;

public class HoverTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public HandManager handManager;

    public void OnPointerEnter(PointerEventData eventData)
    {
        //handManager.MoveUp();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //handManager.MoveDown();
    }
}
