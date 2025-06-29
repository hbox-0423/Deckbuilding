using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    //public GameObject cardPrefab;
    public Transform handArea;
    public float radius = 400f;
    public int maxHandSize = 6;

    private List<Transform> cardsInHand = new List<Transform>();
    [SerializeField] private List<CardData> cards = new List<CardData>();
    [SerializeField] private Pooling cardPool;
    public void DrawCard()
    {
        if(cardsInHand.Count >= maxHandSize)
        {
            Debug.Log("ÇÚµå°¡ °¡µæ Ã¡½À´Ï´Ù!");
            return;
        }
        //GameObject newCard = Instantiate(cardPrefab, handArea);
        GameObject newCard = cardPool.Get();
        newCard.transform.SetParent(handArea,false);
        newCard.transform.localScale = Vector3.one;

        newCard.transform.localPosition = Vector3.zero;
        newCard.transform.localRotation = Quaternion.identity;

        //newCard.GetComponent<CardController>().Init(cards[Random.Range(0, cards.Count)]);
        //newCard.GetComponent<CardController>().ShowCost();
        CardData data = cards[Random.Range(0, cards.Count)];
        CardController controller = newCard.GetComponent<CardController>();
        controller.Init(data);
        controller.ShowCost();

        CardMovement move = newCard.GetComponent<CardMovement>();
        if (move != null)
            move.targetPosition = Vector2.zero;

        cardsInHand.Add(newCard.transform);

        ArrangeCards();
    }

    public void ArrangeCards()
    {
        int count = cardsInHand.Count;
        if (count == 0) return;

        float angleRange = Mathf.Min(10f * count, 50f);
        float angleStep = (count > 1) ? angleRange / (count - 1) : 0f;
        float startAngle = -angleRange / 2f;

        for (int i = 0; i < count; i++)
        {
            float angle = startAngle + angleStep * i;
            float rad = Mathf.Deg2Rad * angle;

            Vector2 pos = new Vector2(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius - radius);

            
            CardMovement cardMove = cardsInHand[i].GetComponent<CardMovement>();
            if (cardMove != null)
                cardMove.SetTargetPosition(pos);

            float rotationZ = -angle * 0.5f;
            cardsInHand[i].localRotation = Quaternion.Euler(0f, 0f, rotationZ);

            cardsInHand[i].SetSiblingIndex(i);
        }
    }

    public void RemoveCard(Transform card)
    {
        cardsInHand.Remove(card);
        ArrangeCards();
    }

}
