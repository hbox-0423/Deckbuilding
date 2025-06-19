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
        Debug.Log($"���� ����: {damage}, ���� ü��: {currentHp}");
        if ( currentHp <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log($"{this.name} ���");
        Destroy(gameObject);
    }
}
