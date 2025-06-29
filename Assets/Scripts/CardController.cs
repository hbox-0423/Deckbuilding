using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEditor.U2D;

public class CardController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public int ID;
    public string Name;
    public int Type;
    public int Cost;
    public int Value;
    public string Tooltip;
    public Image CardImage;

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
        this.CardImage.sprite = cardData.Sprite;
    }

    public void ShowCost()
    {
        nameText.text = Name.ToString();
        CostText.text = Cost.ToString();
        tooltipText.text = Tooltip.ToString();
        CardImage.sprite = CardImage.sprite;
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
        
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        LayerMask monsterLayer = LayerMask.GetMask("Monster");

        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero, 0f, monsterLayer);

        LayerMask playerLayer = LayerMask.GetMask("player");

        RaycastHit2D self = Physics2D.Raycast(worldPos, Vector2.zero, 0f, playerLayer);

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
                    //Destroy(gameObject);
                    this.gameObject.SetActive(false);
                }
            }
        }
        else if (self.collider != null)
        {
            Debug.Log("버프 사용!");
            if (playerController.UseCost(Cost))
            {
                PlayerController player =  self.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    HandManager.RemoveCard(this.transform);
                    //Destroy(gameObject);
                    this.gameObject.SetActive(false);
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
