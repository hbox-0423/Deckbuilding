using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int Cost = 1;

    private Vector2 defaultPos;
    private Quaternion defaultRot;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PlayerController playerController;
    public TextMeshProUGUI CostText;
    public HandManager HandManager;
    private CardMovement cardMovement;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        playerController = FindAnyObjectByType<PlayerController>();
        HandManager = FindAnyObjectByType<HandManager>();
        cardMovement = GetComponent<CardMovement>();
        ShowCost();
    }
    public void ShowCost()
    {
        if(CostText != null)
        {
            CostText.text = Cost.ToString();
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        cardMovement.isBeingDragged = true;

        defaultPos = rectTransform.anchoredPosition;
        defaultRot = rectTransform.localRotation;
        canvasGroup.blocksRaycasts = false;

        rectTransform.localRotation = Quaternion.identity;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        cardMovement.isBeingDragged = false;

        canvasGroup.blocksRaycasts = true;

        // 새 Input System 사용 시 마우스 위치 가져오기
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // 2D 레이어 마스크 (Monster 레이어만 검사)
        LayerMask monsterLayer = LayerMask.GetMask("Monster");

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, monsterLayer);

        if (hit.collider != null)
        {
            Debug.Log("몬스터 감지!");
            if (playerController.UseCost(Cost))
            {
                MonsterController monster = hit.collider.GetComponent<MonsterController>();
                if (monster != null)
                {
                    monster.TakeDamage(10);  // 데미지 10 고정 예시
                    HandManager.RemoveCard(this.transform);
                    Destroy(gameObject);
                }
            }

        }
        else
        {
            Debug.Log("몬스터 없음");
        }

        // 드래그 끝났으니 카드 원위치
        rectTransform.anchoredPosition = defaultPos;
        rectTransform.localRotation = defaultRot;
    }
}
