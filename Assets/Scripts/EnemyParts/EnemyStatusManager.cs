using UnityEngine;

// 雑魚敵のステータスのみを管理したい
public class EnemyStatusManager : MonoBehaviour
{
    public int Type;
    public int HP;
    private int Power;
    private float Speed;

    public void SetStatus(int hp, int power, float speed)
    {
        HP = hp; Power = power; Speed = speed;
    }

    public int GetHP()
    {
        return HP;
    }

    public void SetHP(int PlayerPower)
    {
        HP -= PlayerPower;
    }

    public int GetPower()
    {
        return Power;
    }

    public float GetSpeed()
    {
        return Speed;
    }
}