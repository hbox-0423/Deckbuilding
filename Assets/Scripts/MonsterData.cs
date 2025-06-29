using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Monster/MonsterData")]
public class MonsterData : ScriptableObject
{
    public string Name;
    public int HP;

    public void Init(string name, int HP)
    {
        this.Name = name;
        this.HP = HP;
    }
}
