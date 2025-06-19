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
            Debug.Log($"�ڽ�Ʈ ���: {cost} ���� �ڽ�Ʈ: {currentCost}");
            return true;
        }
        else
        {
            Debug.Log("�ڽ�Ʈ ����!");
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
