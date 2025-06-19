using UnityEngine;

public class TurnEnd : MonoBehaviour
{
    public TurnManager turnManager;

    public void OnEndTurnButtonPressed()
    {
        turnManager.EndPlayerTurn();
    }
}
