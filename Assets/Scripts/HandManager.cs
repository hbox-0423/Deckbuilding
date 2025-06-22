using UnityEngine;
using System.Collections.Generic;

public class HandManager : MonoBehaviour
{
    public GameObject cardPrefab;
    public Transform handArea;
    public float radius = 400f;
    public int maxHandSize = 6;

    private List<Transform> cardsInHand = new List<Transform>();
    [SerializeField] private List<CardData> cards = new List<CardData>();
    public void DrawCard()
    {
        if(cardsInHand.Count >= maxHandSize)
        {
            Debug.Log("ÇÚµå°¡ °¡µæ Ã¡½À´Ï´Ù!");
            return;
        }
        GameObject newCard = Instantiate(cardPrefab, handArea);
        newCard.GetComponent<CardController>().Init(cards[Random.Range(0, cards.Count)]);
        newCard.GetComponent<CardController>().ShowCost();
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
        }
    }

    public void RemoveCard(Transform card)
    {
        cardsInHand.Remove(card);
        ArrangeCards();
    }
}
