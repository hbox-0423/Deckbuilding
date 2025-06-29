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
        Debug.Log("플레이어 턴 시작");

        playerController.ResetCost();
        for (int i = 0; i < 4; i++)
        {
            handManager.DrawCard();
        }

    }
    public void StartPlayerTurn()
    {
        currentState = TurnState.PlayerTurn;
        Debug.Log("플레이어 턴 시작");

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
        Debug.Log("플레이어 턴 종료");


        endTurnButton.interactable = false;
        Invoke("StartEnemyTurn", 1.0f); // 1초 후 몬스터 턴 시작
        state.text = "Enemy's turn...";
    }

    void StartEnemyTurn()
    {
        Debug.Log("몬스터 턴 시작");

        //몬스터 행동
        Invoke("EndEnemyTurn", 1.0f);
    }

    void EndEnemyTurn()
    {
        Debug.Log("몬스터 턴 종료");
        StartPlayerTurn();
    }
}
