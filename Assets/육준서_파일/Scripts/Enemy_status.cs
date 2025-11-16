using UnityEngine;

[CreateAssetMenu(fileName = "Enemy_status", menuName = "Scriptable Objects/Enemy_status")]
public class Enemy_status : ScriptableObject
{
    public float speed;
    public float attackCoolTime;
    public float hp;
    public float AttackDamage;
    public float wallDamage;
    public float attackDelay;
}
