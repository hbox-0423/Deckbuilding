using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public Button endTurnButton;
    public TMP_Text state;
    public PlayerController playerController;
    public HandManager handManager;
    public enum TurnState
    {
        PlayerTurn,
        EnemyTurn
    }

    public TurnState currentState;

    void Start()
    {
        FirstTurn();
    }

    public void FirstTurn()
    {
        currentState = TurnState.PlayerTurn;
        Debug.Log("�÷��̾� �� ����");

        playerController.ResetCost();
        for (int i = 0; i < 4; i++)
        {
            handManager.DrawCard();
        }

    }
    public void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;
        Debug.Log("�÷��̾� �� ����");

        playerController.ResetCost();
        for (int i = 0; i < 2; i++)
        {
            handManager.DrawCard();
        }


        endTurnButton.interactable = true;
        state.text = "Turn End";
    }

    public void EndPlayerTurn()
    {
        currentState = TurnState.EnemyTurn;
        Debug.Log("�÷��̾� �� ����");


        endTurnButton.interactable = false;
        Invoke("StartEnemyTurn", 1.0f); // 1�� �� ���� �� ����
        state.text = "Enemy's turn...";
    }

    void StartEnemyTurn()
    {
        Debug.Log("���� �� ����");

        //���� �ൿ
        Invoke("EndEnemyTurn", 1.0f);
    }

    void EndEnemyTurn()
    {
        Debug.Log("���� �� ����");
        StartPlayerTurn();
    }
}
