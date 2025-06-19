using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public int MaxHp = 30;
    public int currentHp;

    private void Start()
    {
        currentHp = MaxHp;
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log($"몬스터 피해: {damage}, 남은 체력: {currentHp}");
        if ( currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{this.name} 사망");
        Destroy(gameObject);
    }
}
