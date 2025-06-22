using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int ID;
    public string Name;
    public int Type;
    public int Cost;
    public int Value;
    public string Tooltip;

    private Vector2 defaultPos;
    private Quaternion defaultRot;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private PlayerController playerController;
    public TextMeshProUGUI CostText;
    public TextMeshProUGUI tooltipText;
    public TextMeshProUGUI nameText;
    public HandManager HandManager;
    private CardMovement cardMovement;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        playerController = FindAnyObjectByType<PlayerController>();
        HandManager = FindAnyObjectByType<HandManager>();
        cardMovement = GetComponent<CardMovement>();
    }

    public void Init(CardData cardData)
    {
        this.ID = cardData.ID;
        this.Name = cardData.Name;
        this.Type = cardData.Type;
        this.Cost = cardData.Cost;
        this.Value = cardData.Value;
        this.Tooltip = cardData.Tooltip;
    }

    public void ShowCost()
    {
        nameText.text = Name.ToString();
        CostText.text = Cost.ToString();
        tooltipText.text = Tooltip.ToString();
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

        // 2D ���̾� ����ũ
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
