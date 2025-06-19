using UnityEngine;

public class CardMovement : MonoBehaviour
{
    public Vector2 targetPosition;
    public float moveSpeed = 10f;
    public bool isBeingDragged = false;

    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (!isBeingDragged)
        {
            rectTransform.anchoredPosition = Vector2.Lerp(rectTransform.anchoredPosition, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    public void SetTargetPosition(Vector2 pos)
    {
        targetPosition = pos;
    }
}
