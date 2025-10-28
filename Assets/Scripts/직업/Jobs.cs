using UnityEngine;

[CreateAssetMenu(fileName = "Jobs", menuName = "Scriptable Objects/Jobs")]
public class Jobs : ScriptableObject
{
    [Header("직업의 기본 정보")]
    public string jobName;                  // 직업 이름
    // [TextArea] public string description;// 설명 넣을 거임?

    [Header("기본 스탯")]
    public int baseHP;                      // 기본 체력
    public int baseAttackDamage;            // 기본 공격력
    public int baseAttackSpeed;             // 기본 공격 속도
    // public int baseDefense;              방어력은 추후에 추가
    public int baseSpeed;                   // 기본 이동속도
    public int baseMana;                    // 기본 마나

    [Header("영혼 흡수 성장 배율")]
    public float soulBonusMultiplier = 1f;
}
