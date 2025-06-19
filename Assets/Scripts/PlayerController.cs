using TMPro;
using UnityEngine;
using UnityEngine.Accessibility;

public class PlayerController : MonoBehaviour
{
    public int MaxCost = 6;
    public int currentCost;

    public TMP_Text HaveCost;

    public static object Instance { get; internal set; }

    void Start()
    {
        ResetCost();
    }

    public bool UseCost(int cost)
    {
        if (currentCost >= cost)
        {
            currentCost -= cost;
            UpdateCost();
            Debug.Log($"코스트 사용: {cost} 남은 코스트: {currentCost}");
            return true;
        }
        else
        {
            Debug.Log("코스트 부족!");
            return false;
        }
    }

    public void ResetCost()
    {
        currentCost = MaxCost;
        UpdateCost();
    }

    public void UpdateCost()
    {
        HaveCost.text = $"{currentCost}/{MaxCost}";
    }
}
