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

        // �� Input System ��� �� ���콺 ��ġ ��������
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        // 2D ���̾� ����ũ (Monster ���̾ �˻�)
        LayerMask monsterLayer = LayerMask.GetMask("Monster");

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, monsterLayer);

        if (hit.collider != null)
        {
            Debug.Log("���� ����!");
            if (playerController.UseCost(Cost))
            {
                MonsterController monster = hit.collider.GetComponent<MonsterController>();
                if (monster != null)
                {
                    monster.TakeDamage(10);  // ������ 10 ���� ����
                    HandManager.RemoveCard(this.transform);
                    Destroy(gameObject);
                }
            }

        }
        else
        {
            Debug.Log("���� ����");
        }

        // �巡�� �������� ī�� ����ġ
        rectTransform.anchoredPosition = defaultPos;
        rectTransform.localRotation = defaultRot;
    }
}
