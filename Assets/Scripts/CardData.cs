using UnityEngine;

[CreateAssetMenu(fileName = "CardData", menuName = "Cards/CardData")]
public class CardData : ScriptableObject
{
    public int ID;
    public string Name;
    public int Type;
    public int Cost;
    public int Value;
    public string Tooltip;
    
    public void Init(int id,string name, int type,int cost,int value,string tooltip)
    {
        this.ID = id;
        this.Name = name;
        this.Type = type;
        this.Cost = cost;
        this.Value = value;
        this.Tooltip = tooltip;
    }
}
